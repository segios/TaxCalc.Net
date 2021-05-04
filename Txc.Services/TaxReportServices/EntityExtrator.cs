using System;
using System.Collections.Generic;
using System.Linq;
using Txc.Model;
using Txc.Model.Statement;

namespace Txc.Services.TaxReportServices
{
    public class EntityExtrator<T> : IEntityExtrator<T>
    {
        List<IDataTransformer> transformers = new List<IDataTransformer>();
        public EntityExtrator()
        {
        }

        public virtual void AttachTransfromStep(IDataTransformer transformer)
        {
            transformers.Add(transformer);
        }

        public virtual void Prepare(
            IBrokerParserProvider brokerParserProvider,
            IEnumerable<StatementData> statements, ReportOptions reportOptions, Action<IList<T>> onReadyCallback)
        {
            var entities = Load(brokerParserProvider, statements);

            entities = Filter(reportOptions, entities);

            //IList<object> generalList = entities.Select(x=>(object)x).ToList();
            
            //foreach (var transformer in transformers) 
            //{
            //    generalList = transformer.Transform(generalList);
            //}

            onReadyCallback(entities);
        }

        protected virtual IList<T> Filter(ReportOptions reportOptions, IList<T> entities)
        {
            if (typeof(IOperation).IsAssignableFrom(typeof(T)))
            {
                return entities.Where(x => ((IOperation)x).OperationDate.Year == reportOptions.Year).ToList();
            }

            return entities;
        }

        protected virtual IList<T> Load(IBrokerParserProvider brokerParserProvider, IEnumerable<StatementData> statements)
        {
            var dataReader = brokerParserProvider.EntitiesDataReader;
            List<T> result = new List<T>();

            foreach (var s in statements) 
            {
                var entities = dataReader.Read<T>(s);
                if (entities != null && entities.Any())
                {
                    result.AddRange(entities);
                }
            }

            return result;
        }
    }

    public class EntityExtrator<T, TConverted> : IEntityExtrator<T, TConverted>
        where T : IRateConvertable
        where TConverted : IRateConvertable
    {
        private readonly IEntityExtrator<T> internalExtractor;
        private readonly IEntityConvertor<T> convertor;

        public EntityExtrator(IEntityExtrator<T> internalExtractor, IEntityConvertor<T> convertor)
        {
            this.internalExtractor = internalExtractor;
            this.convertor = convertor;
        }

        public virtual void Prepare(
            IBrokerParserProvider brokerParserProvider,
            IEnumerable<StatementData> statements, ReportOptions reportOptions, Action<IList<TConverted>> onReadyCallback)
        {
            IList<T> entities = null;
            internalExtractor.Prepare(brokerParserProvider, statements, reportOptions, (data) => entities = data);

            var convertedEntitties = Convert(reportOptions, entities);
            onReadyCallback(convertedEntitties);
        }

        protected virtual IList<TConverted> Convert(ReportOptions reportOptions, IList<T> entities)
        {
            var result = new List<TConverted>();

            Func<ConvertedEntity<T>, TConverted> CreateConverted = (entity) =>
            {
                return (TConverted)Activator.CreateInstance(typeof(TConverted), entity);
            };

            result = convertor.Convert(reportOptions.Profile.TaxCurrency, entities)
                    .Select(x => CreateConverted(x)).ToList();

            return result;
        }
    }
}

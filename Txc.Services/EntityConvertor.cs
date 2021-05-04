using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Txc.Model;

namespace Txc.Services
{

    public class EntityConvertor<T> : IEntityConvertor<T>
            where T : IRateConvertable
    {
        private IRateService rateService;
        public EntityConvertor(IRateService rateService)
        {
            this.rateService = rateService;
        }

        public ConvertedEntity<T> Convert(string localCurrency, T entity) 
        {
            var rate = rateService.GetRate(entity.Currency, localCurrency, entity.OperationDate);
            if (rate == null)
            {
                // throw new TaxCalcException($"Rate for {entity.Currency} at date not found {entity.OperationDate}");
            }

            var convertableEntity = new ConvertedEntity<T>(entity, localCurrency, rate ?? 0);

            return convertableEntity;
        }

        public List<ConvertedEntity<T>> Convert(string localCurrency, IEnumerable<T> entities)
        {
            var res = new List<ConvertedEntity<T>>();

            foreach (var entity in entities) 
            {
                var convertableEntity = Convert(localCurrency, entity);
                res.Add(convertableEntity);
            }

            return res;
        }

    }
}

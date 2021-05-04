using System;
using System.Collections.Generic;
using Txc.Model;
using Txc.Model.Statement;

namespace Txc.Services.TaxReportServices
{
    public interface IEntityExtrator
    {
        void Prepare(IBrokerParserProvider brokerParserProvider,
            IEnumerable<StatementData> statements,
            ReportOptions reportOptions,
            Action<IList<object>> onReadyCallback
            );

        void AttachTransfromStep(IDataTransformer transformer);
    }

    public interface IEntityExtrator<T> //: IEntityExtrator
    {
        void Prepare(IBrokerParserProvider brokerParserProvider,
            IEnumerable<StatementData> statements,
            ReportOptions reportOptions,
            Action<IList<T>> onReadyCallback);

    }

    public interface IEntityExtrator<T, TConverted> 
        where TConverted : IRateConvertable
    {
        void Prepare(IBrokerParserProvider brokerParserProvider,
            IEnumerable<StatementData> statements, 
            ReportOptions reportOptions, 
            Action<IList<TConverted>> onReadyCallback);
    }
}

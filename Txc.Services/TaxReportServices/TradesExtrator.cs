using System.Collections.Generic;
using System.Linq;
using Txc.Model;
using Txc.Model.Statement;
using Txc.Model.Trades;

namespace Txc.Services.TaxReportServices
{
    public class TradesExtrator
    {
        private readonly IEntityConvertor<Trade> tradeConvertor;
        private readonly IDataTransformer<Trade, ClosedOperation<Trade>> tradeTransformer;
        private readonly ITaxReport taxReport;
        private IBrokerParserProvider brokerParserProvider;

        public TradesExtrator(
            ITaxReport taxReport,
            IBrokerParserProvider brokerParserProvider,
            IDataTransformer<Trade, ClosedOperation<Trade>> tradeTransformer,
            IEntityConvertor<Trade> tradeConvertor
            )
        {
            this.taxReport = taxReport;
            this.brokerParserProvider = brokerParserProvider;
            this.tradeConvertor = tradeConvertor;
            this.tradeTransformer = tradeTransformer;
        }

        public void Prepare(List<StatementData> statements, ReportOptions reportOptions) 
        {
            var trades = Consolidate(statements);

            trades = trades
                .OrderBy(x => x.AssetCategoryType)
                .ThenBy(x => x.Symbol)
                .ThenBy(x => x.DateTime)
                .ToList();

            var closedTrades = GetClosedTrades(reportOptions, trades);
            taxReport.TradeData.Trades = Convert(reportOptions, closedTrades);
            
        }

        

        private IList<Trade> Consolidate(List<StatementData> statements)
        {
            var dataReader = brokerParserProvider.EntitiesDataReader;
            List<Trade> result = new List<Trade>();
            statements.ForEach(s => {
                var trades = dataReader.Read<Trade>(s);
                if (trades != null)
                {
                    result.AddRange(trades);
                }
            });

            return result;
        }


        private IList<ClosedOperation<ConvertedTrade>> Convert(ReportOptions reportOptions, IList<ClosedOperation<Trade>> trades)
        {
            var convertedTrades = new List<ClosedOperation<ConvertedTrade>>();
            foreach (var ct in trades)
            {
                var convertedOperation = new ClosedOperation<ConvertedTrade>();

                convertedOperation.OpenTrades.AddRange(tradeConvertor.Convert(reportOptions.Profile.TaxCurrency, ct.OpenTrades)
                    .Select(x => new ConvertedTrade(x)));
                convertedOperation.CloseTrade = new ConvertedTrade(tradeConvertor.Convert(reportOptions.Profile.TaxCurrency, ct.CloseTrade));

                convertedOperation.Validate();

                convertedTrades.Add(convertedOperation);
            }

            return convertedTrades;
        }

        private IList<ClosedOperation<Trade>> GetClosedTrades(ReportOptions reportOptions, IList<Trade> trades)
        {
            var closedTrades = tradeTransformer.Transform(trades);
            return closedTrades.Where(x => x.CloseTrade.DateTime.Year == reportOptions.Year).ToList();
        }
    }
}

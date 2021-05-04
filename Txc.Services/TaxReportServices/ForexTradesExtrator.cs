using System.Collections.Generic;
using System.Linq;
using Txc.Model;
using Txc.Model.ForexTrades;
using Txc.Model.Statement;
using Txc.Model.Trades;

namespace Txc.Services.TaxReportServices
{
    public class ForexTradesExtrator
    {
        private readonly IEntityConvertor<ForexTrade> tradeConvertor;
        private readonly IDataTransformer<ForexTrade, ClosedOperation<ForexTrade>> tradeTransformer;
        private readonly ITaxReport taxReport;
        private IBrokerParserProvider brokerParserProvider;

        public ForexTradesExtrator(
            ITaxReport taxReport,
            IBrokerParserProvider brokerParserProvider,
            IDataTransformer<ForexTrade, ClosedOperation<ForexTrade>> tradeTransformer,
            IEntityConvertor<ForexTrade> tradeConvertor
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

            // update currency of forex to usd to properly get convertion rate
            foreach(var trade in trades)
            {
                trade.Currency = reportOptions.BaseCurrency;
            }

            var closedTrades = GetClosedTrades(reportOptions, trades);

            var closedConvertedTrades = Convert(reportOptions, closedTrades);
            var result = new List<ConvertedForexTrade>();
            foreach(var ct in closedConvertedTrades)
            {
                result.AddRange(ct.OpenTrades);
                result.Add(ct.CloseTrade);
            }

            taxReport.ForexTrades = result;
        }

        private IList<ForexTrade> Consolidate(List<StatementData> statements)
        {
            var dataReader = brokerParserProvider.EntitiesDataReader;
            List<ForexTrade> result = new List<ForexTrade>();
            statements.ForEach(s => {
                var trades = dataReader.Read<ForexTrade>(s);
                if (trades != null)
                {
                    result.AddRange(trades);
                }
            });

            return result;
        }


        private IList<ClosedOperation<ConvertedForexTrade>> Convert(ReportOptions reportOptions, IList<ClosedOperation<ForexTrade>> trades)
        {
            var convertedTrades = new List<ClosedOperation<ConvertedForexTrade>>();
            foreach (var ct in trades)
            {
                var convertedOperation = new ClosedOperation<ConvertedForexTrade>();

                convertedOperation.OpenTrades.AddRange(tradeConvertor.Convert(reportOptions.Profile.TaxCurrency, ct.OpenTrades)
                    .Select(x => new ConvertedForexTrade(x)));
                convertedOperation.CloseTrade = new ConvertedForexTrade(tradeConvertor.Convert(reportOptions.Profile.TaxCurrency, ct.CloseTrade));

                //convertedOperation.Validate();

                convertedTrades.Add(convertedOperation);
            }

            return convertedTrades;
        }

        private IList<ClosedOperation<ForexTrade>> GetClosedTrades(ReportOptions reportOptions, IList<ForexTrade> trades)
        {
            var closedTrades = tradeTransformer.Transform(trades);
            return closedTrades.Where(x => x.CloseTrade.DateTime.Year == reportOptions.Year).ToList();
        }
    }
}

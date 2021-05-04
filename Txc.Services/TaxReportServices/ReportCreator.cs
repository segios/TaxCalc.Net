using System.Linq;
using System.Collections.Generic;
using Txc.Model;
using Txc.Model.Dividends;
using Txc.Model.Fees;
using Txc.Model.FinInstruments;
using Txc.Model.Interests;
using Txc.Model.SecuritiesLent;
using Txc.Model.Statement;
using Txc.Model.Trades;
using System;
using Txc.Model.Deposits;
using Txc.Model.ForexTrades;
using Txc.Model.AccountInformations;

namespace Txc.Services.TaxReportServices
{

    public class ReportCreator: IReportCreator
    {
        private readonly IBrokerParserProviderFactory brokerParserProviderFactory;
        private readonly IDataTransformerFactory dataTransformerFactory;
        
        private readonly IEntityConvertor<Trade> tradeConvertor;
        private readonly IEntityConvertor<ForexTrade> forexTradeConvertor;
        private readonly IEntityExtrator<Deposit> depositsExtrator;
        private readonly IEntityExtrator<FinInstrument> finInstrumentsExtrator;
        private readonly IEntityExtrator<AccountInformation> accountInformationExtrator;
        private readonly IEntityExtrator<SecuritiesLentInterest, ConvertedSecuritiesLentInterest> securitiesLentExtrator;
        private readonly IEntityExtrator<Dividend, ConvertedDividend> dividendsEntityExtrator;
        private readonly IEntityExtrator<Interest, ConvertedInterest> interestEntityExtrator;
        private readonly IEntityExtrator<Fee, ConvertedFee> feeEntityExtrator;
        private IBrokerParserProvider brokerParserProvider;
        public ReportCreator(
            IBrokerParserProviderFactory brokerParserProviderFactory, 
            IDataTransformerFactory dataTransformerFactory,

            IEntityConvertor<Trade> tradeConvertor,
            IEntityConvertor<ForexTrade> forexTradeConvertor,

            IEntityExtrator<FinInstrument> finInstrumentsExtrator, 
            IEntityExtrator<AccountInformation> accountInformationExtrator,

            IEntityExtrator<Deposit> depositsExtrator,
            IEntityExtrator<SecuritiesLentInterest, ConvertedSecuritiesLentInterest> securitiesLentExtrator,
            IEntityExtrator<Dividend, ConvertedDividend> dividendsEntityExtrator,
            IEntityExtrator<Interest, ConvertedInterest> interestEntityExtrator,
            IEntityExtrator<Fee, ConvertedFee> feeEntityExtrator
            )
        {
            this.brokerParserProviderFactory = brokerParserProviderFactory;
            this.dataTransformerFactory = dataTransformerFactory;
            this.tradeConvertor = tradeConvertor;
            this.forexTradeConvertor = forexTradeConvertor;
            this.depositsExtrator = depositsExtrator;
            this.finInstrumentsExtrator = finInstrumentsExtrator;
            this.accountInformationExtrator = accountInformationExtrator;
            this.securitiesLentExtrator = securitiesLentExtrator;
            this.dividendsEntityExtrator = dividendsEntityExtrator;
            this.interestEntityExtrator = interestEntityExtrator;
            this.feeEntityExtrator = feeEntityExtrator;
        }

        private List<StatementData> ParseStatements(ReportOptions reportOptions) 
        {
            brokerParserProvider = brokerParserProviderFactory.CreateNew(reportOptions.BrokerCode.ToString());

            var statementParser = brokerParserProvider.StatementParser;
            var statements = new List<StatementData>();
            
            var profile = reportOptions.Profile;

            foreach (var stmData in profile.Statements)
            {
                var stm = statementParser.ParseStatement(stmData);
                statements.Add(stm);
            }

            return statements;
        }

        
        public ITaxReport BuildReport(ReportOptions reportOptions)
        {
            brokerParserProvider = brokerParserProviderFactory.CreateNew(reportOptions.BrokerCode.ToString());

            if (brokerParserProvider == null) 
            {
                throw new TaxCalcException($"BrokerParserProvider '{reportOptions.BrokerCode}' not registered");
            }

            TaxReport taxReport = new TaxReport(reportOptions);
            var statements = ParseStatements(reportOptions);

            var brokerReportEntities = reportOptions.ReportEntitities;
            
            AccountInformationGroup accountInformationGroup = null;

            accountInformationExtrator.Prepare(brokerParserProvider, statements.Take(1), reportOptions,
                    (accInfo) => accountInformationGroup = new AccountInformationGroup(accInfo));

            var currency = accountInformationGroup.Currency;
            if (!string.IsNullOrEmpty(currency))
            {
                reportOptions.BaseCurrency = currency;
            }

            if (brokerReportEntities.HasFlag(EntityCode.FinInstruments))
            {
                finInstrumentsExtrator.Prepare(brokerParserProvider, statements, reportOptions,
                    (finInstr) => taxReport.FinInstruments = finInstr);
            }

            if (brokerReportEntities.HasFlag(EntityCode.Trades))
            {
                FillTrades(reportOptions, taxReport, statements);
                taxReport.TradeData.TradeAggregations = AggregateTrades(reportOptions, taxReport, taxReport.TradeData.Trades);
            }

            if (brokerReportEntities.HasFlag(EntityCode.Forex))
            {
                //FillForexTrades(reportOptions, taxReport, statements);
            }

            if (brokerReportEntities.HasFlag(EntityCode.Dividends))
            {
                dividendsEntityExtrator.Prepare(brokerParserProvider, statements, reportOptions,
                    (dividends) => {
                        foreach (var div in dividends)
                        {
                            div.Entity.SymbolName = taxReport.FindFinInstrument(div.Symbol)?.Description ?? div.Symbol;
                        }
                        taxReport.Dividends = dividends;
                });
            }

            if (brokerReportEntities.HasFlag(EntityCode.SecuritiesLentInterests))
            {

                securitiesLentExtrator.Prepare(brokerParserProvider, statements, reportOptions,
                    (securitiesLentInterests) => taxReport.SecuritiesLentInterests = securitiesLentInterests);
            }

            if (brokerReportEntities.HasFlag(EntityCode.Interests))
            {
                interestEntityExtrator.Prepare(brokerParserProvider, statements, reportOptions, 
                    (interests) => taxReport.Interests = interests);
            }

            if (brokerReportEntities.HasFlag(EntityCode.Fees))
            {
                feeEntityExtrator.Prepare(brokerParserProvider, statements, reportOptions, 
                    (fees) => taxReport.Fees = fees);
            }

            if (brokerReportEntities.HasFlag(EntityCode.Deposits))
            {
                depositsExtrator.Prepare(brokerParserProvider, statements, reportOptions,
                    (deposits) => taxReport.Deposits = deposits);
            }


            return taxReport;
        }
        
        private void FillForexTrades(ReportOptions reportOptions, ITaxReport taxReport, List<StatementData> statements)
        {
            var tradeTransformer = dataTransformerFactory.GetInstance<IDataTransformer<ForexTrade, ClosedOperation<ForexTrade>>>(typeof(IDataTransformer<ForexTrade, ClosedOperation<ForexTrade>>));
            var extrator = new ForexTradesExtrator(taxReport, brokerParserProvider, tradeTransformer, forexTradeConvertor);
            extrator.Prepare(statements, reportOptions);
        }


        private void FillTrades(ReportOptions reportOptions, ITaxReport taxReport, List<StatementData> statements)
        {
            var tradeTransformer = dataTransformerFactory.GetInstance<IDataTransformer<Trade, ClosedOperation<Trade>>>(typeof(IDataTransformer<Trade, ClosedOperation<Trade>>));
            var extrator = new TradesExtrator(taxReport, brokerParserProvider, tradeTransformer, tradeConvertor);
            extrator.Prepare(statements, reportOptions);
        }

        private IList<TradeAggregation> AggregateTrades(
            ReportOptions reportOptions,
            ITaxReport taxReport,
            IList<ClosedOperation<ConvertedTrade>> convertedTrades)
        {
            List<TradeAggregation> tradeAggregations = new List<TradeAggregation>();
            var symbol = string.Empty;
            TradeAggregation tradeAggregation = null;

            Action<ClosedOperation <ConvertedTrade>, TradeAggregation, ConvertedEntity<Trade>> appendToAggregation = (closedOp, tradeAggregation, trade) =>
            {
                if (trade.Entity.TradeOperation == TradeOperation.Buy)
                {
                    tradeAggregation.BuyBasis += Math.Abs(trade.Entity.BasisAmount);
                    tradeAggregation.BuyConvertedBasis += Math.Abs(trade.ConvertedBasis);

                }
                else if (trade.Entity.TradeOperation == TradeOperation.Sell)
                {
                    if (trade.Entity.AssetCategoryType == AssetCategoryType.Options && 
                                    closedOp.IsClosed && closedOp.CloseTrade.ConvertedBasis == 0)
                    {
                        // for options sells comm will go to buy side (so buy side will not be  0 for sold options)
                        tradeAggregation.SellBasis += Math.Abs(trade.Entity.Proceeds);
                        tradeAggregation.SellConvertedBasis += Math.Abs(trade.ConvertedGrossAmount);

                        tradeAggregation.BuyBasis += Math.Abs(trade.Entity.Comm);
                        tradeAggregation.BuyConvertedBasis += Math.Abs(trade.ConvertedComm);
                    }
                    else 
                    {
                        tradeAggregation.SellBasis += Math.Abs(trade.Entity.BasisAmount);
                        tradeAggregation.SellConvertedBasis += Math.Abs(trade.ConvertedBasis);
                    }
                }
            };

            foreach (var ct in convertedTrades)
            {
                if (!ct.IsClosed)
                {
                    continue;
                }

                if (ct.Symbol != symbol)
                {
                    symbol = ct.Symbol;
                    tradeAggregation = new TradeAggregation();
                    tradeAggregation.Symbol = symbol;
                    tradeAggregation.SymbolName = taxReport.FindFinInstrument(symbol)?.Description ?? symbol;


                    if (ct.IsClosed)
                    {
                        tradeAggregation.AssetCategoryType = ct.CloseTrade.Entity.AssetCategoryType;
                        tradeAggregation.BaseCurrency = ct.CloseTrade.Currency;
                    }
                    tradeAggregation.Currency = reportOptions.Profile.TaxCurrency;

                    tradeAggregations.Add(tradeAggregation);
                }

                if (ct.OpenTrades.Any())
                {
                    foreach (var trade in ct.OpenTrades)
                    {
                        appendToAggregation(ct, tradeAggregation, trade);
                    }
                }

                if (ct.IsClosed)
                {
                    appendToAggregation(ct, tradeAggregation, ct.CloseTrade);
                }
            }

            return tradeAggregations;
        }
    }
}

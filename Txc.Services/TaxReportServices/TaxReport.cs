using System.Collections.Generic;
using System.Linq;
using Txc.Model;
using Txc.Model.Account;
using Txc.Model.Deposits;
using Txc.Model.Dividends;
using Txc.Model.Fees;
using Txc.Model.FinInstruments;
using Txc.Model.ForexTrades;
using Txc.Model.Interests;
using Txc.Model.SecuritiesLent;

namespace Txc.Services.TaxReportServices
{
    public class TaxReport : ITaxReport
    {
        public ReportOptions ReportOptions { get; set; }

        public TaxReport(ReportOptions reportOptions) {
            this.ReportOptions = reportOptions;
        }

        public IList<ConvertedForexTrade> ForexTrades { get; set; } = new List<ConvertedForexTrade>();
        public IList<Deposit> Deposits { get; set; } = new List<Deposit>();
        public IList<ConvertedFee> Fees { get; set; } = new List<ConvertedFee>();
        public IList<ConvertedInterest> Interests { get; set; } = new List<ConvertedInterest>();
        public IList<ConvertedSecuritiesLentInterest> SecuritiesLentInterests { get; set; } = new List<ConvertedSecuritiesLentInterest>();
        public IList<FinInstrument> FinInstruments { get; set; } = new List<FinInstrument>();
        public IList<ConvertedDividend> Dividends { get; set; } = new List<ConvertedDividend>();
        public FinInstrument FindFinInstrument(string symbol) 
        {
            return FinInstruments.FirstOrDefault(x => x.Symbol == symbol);
        }

        public TradeData TradeData { get; } = new TradeData();

        public IEnumerable<(Tax tax, decimal taxValue)> CalcTradesTaxes()
        {
            if (!TradeData.TradeAggregations.Any())
            {
                return null;
            }
            else
            {
                var result = TradeData.TradeAggregations.Sum(x => x.ResultConvertedBasis);
                var taxProfile = ReportOptions.Profile.TaxesProfiles[TaxClassCodes.General];
                return taxProfile.ApplyTaxes(result);
            }
        }

        public IEnumerable<(Tax tax, decimal taxValue)> CalcDividendsTaxes()
        {
            if (!Dividends.Any())
            {
                return null;
            }
            else
            {
                var result = Dividends.Sum(x => x.ConvertedBasis);
                var taxProfile = ReportOptions.Profile.TaxesProfiles[TaxClassCodes.Dividends];
                return taxProfile.ApplyTaxes(result);
            }
        }

        public IEnumerable<(Tax tax, decimal taxValue)> CalcInterestsTaxes()
        {
            if (!Interests.Any())
            {
                return null;
            }
            else
            {
                var result = Interests.Sum(x => x.ConvertedBasis);
                var taxProfile = ReportOptions.Profile.TaxesProfiles[TaxClassCodes.General];
                return taxProfile.ApplyTaxes(result);
            }
        }
    }
}

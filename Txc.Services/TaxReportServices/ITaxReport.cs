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
    public interface ITaxReport
    {
        ReportOptions ReportOptions { get; set; }
        IList<ConvertedForexTrade> ForexTrades { get; set; }
        IList<Deposit> Deposits { get; set; }
        IList<ConvertedFee> Fees { get; set; }
        IList<ConvertedInterest> Interests { get; set; }
        IList<ConvertedSecuritiesLentInterest> SecuritiesLentInterests { get; set; }
        IList<FinInstrument> FinInstruments { get; set; }
        FinInstrument FindFinInstrument(string symbol);
        TradeData TradeData { get; }
        IList<ConvertedDividend> Dividends { get; set; }

        IEnumerable<(Tax tax, decimal taxValue)> CalcTradesTaxes();
        IEnumerable<(Tax tax, decimal taxValue)> CalcDividendsTaxes();
        IEnumerable<(Tax tax, decimal taxValue)> CalcInterestsTaxes();
    }
}

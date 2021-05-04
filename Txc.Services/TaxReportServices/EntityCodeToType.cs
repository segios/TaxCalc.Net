using System.Collections.Generic;
using Txc.Model;
using Txc.Model.Dividends;
using Txc.Model.Fees;
using Txc.Model.FinInstruments;
using Txc.Model.Interests;
using Txc.Model.SecuritiesLent;
using Txc.Model.Trades;
using System;
using Txc.Model.Deposits;
using Txc.Model.ForexTrades;

namespace Txc.Services.TaxReportServices
{
    public static class EntityCodeToType
    {
        public static Dictionary<EntityCode, Type> map = new Dictionary<EntityCode, Type>()
        {
            { EntityCode.Trades, typeof (Trade) },
            { EntityCode.Dividends, typeof (Dividend) },
            { EntityCode.SecuritiesLentInterests, typeof (SecuritiesLentInterest) },
            { EntityCode.FinInstruments, typeof (FinInstrument) },
            { EntityCode.Interests, typeof (Interest) },
            { EntityCode.Fees, typeof (Fee) },
            { EntityCode.Deposits, typeof (Deposit) },
            { EntityCode.Forex, typeof (ForexTrade) },

        };
    }
}

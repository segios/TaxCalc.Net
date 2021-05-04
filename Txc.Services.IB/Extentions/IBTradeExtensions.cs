using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Txc.Model.Trades;
using Txc.Services.IB.Model;

namespace Txc.Services.IB.Extensions
{
    public static class IBTradeExtensions
    {
        public static Trade ToTrade(this IBTrade ibTrade)
        {
            return new Trade()
            {
                AssetCategoryType = ibTrade.AssetCategoryType,
                Basis = ibTrade.Basis,
                Code = ibTrade.Code,
                Comm = ibTrade.Comm,
                Currency = ibTrade.Currency,
                DateTime = ibTrade.DateTime,
                Proceeds = ibTrade.Proceeds,
                RealizedPL = ibTrade.RealizedPL,
                Quantity = ibTrade.Quantity,
                Symbol = ibTrade.Symbol,
                TPrice = ibTrade.TPrice,
                TradeOperation = ibTrade.TradeOperation,
                TradeType = ibTrade.TradeType,
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Txc.Model.Account;

namespace Txc.Model.Trades
{
    public class TradeAggregationFormatMapping : FormatMapping<TradeAggregation>
    {
        public TradeAggregationFormatMapping()
        {
            var defaultOptions = new FormatOptions(false, false);

            AddKey(("Category", null, (x) => x.AssetCategoryType, new FormatOptions(true, false)));
            AddKey(("Symbol", null, (x) => x.Symbol, defaultOptions));

            AddKey(("SymbolName", null, 
                (x) => x.AssetCategoryType == AssetCategoryType.Stocks ? $"{x.Symbol} ({x.SymbolName})" :x.SymbolName, 
                new FormatOptions(false, false, 50)));
            AddKey(("SellConvertedBasis", null, (x) => x.SellConvertedBasis, defaultOptions));
            AddKey(("BuyConvertedBasis", null, (x) => x.BuyConvertedBasis, defaultOptions));
            AddKey(("ResultConvertedBasis", null, (x) => x.ResultConvertedBasis, defaultOptions));

            AddTotalKey(("ResultConvertedBasis", (x) => x.ResultConvertedBasis));
        }
    }
}

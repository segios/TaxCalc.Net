using System;
using System.ComponentModel;

namespace Txc.Model.Trades
{

    public class TradeAggregation
    {
        [Localizable(true)]
        public AssetCategoryType AssetCategoryType { get; set; }
        public string BaseCurrency { get; set; }
        public string Symbol { get; set; }
        
        public string SymbolName { get; set; }
        public decimal BuyBasis { get; set; }
        public decimal SellBasis { get; set; }
        public decimal ResultBasis
        {
            get
            {
                return SellBasis - BuyBasis;
            }
        }

        // converted info
        public string Currency { get; set; }
        public decimal BuyConvertedBasis { get; set; }
        public decimal SellConvertedBasis { get; set; }

        public decimal ResultConvertedBasis
        {
            get
            {
                return SellConvertedBasis - BuyConvertedBasis;
            }
        }
    }
}

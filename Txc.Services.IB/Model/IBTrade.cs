using System;
using System.Collections.Generic;
using System.Linq;
using Txc.Model;
using Txc.Model.Trades;

namespace Txc.Services.IB.Model
{

    public class IBTrade
    {
        protected static Dictionary<string, TradeType> CodeToTradeType = new Dictionary<string, TradeType>()
        {
            { "O", TradeType.Open },
            { "C", TradeType.Close },
            { "P", TradeType.PartialExecution },
            { "Ep", TradeType.ExpiredPosition },
            { "Ex", TradeType.Exercise },
            { "A", TradeType.Assignment },
        };

        protected static Dictionary<string, AssetCategoryType> AssetCategoryMap = new Dictionary<string, AssetCategoryType>()
        {
            { "Stocks", AssetCategoryType.Stocks },
            { "Equity and Index Options", AssetCategoryType.Options },
            { "Forex", AssetCategoryType.Forex },
        };

        public string AssetCategory { get; set; }
        public AssetCategoryType AssetCategoryType 
        {
            get 
            {
                return AssetCategoryMap.ContainsKey(AssetCategory) ? AssetCategoryMap[AssetCategory] : AssetCategoryType.Unknown;
            }
        }

        public string Currency { get; set; }
        public string Symbol { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Quantity { get; set; }
        public decimal TPrice { get; set; }
        public decimal Proceeds { get; set; }
        public decimal Comm { get; set; }
        public decimal Basis { get; set; }
        public decimal RealizedPL { get; set; }
        public string Code { get; set; }

        private string[] codes;
        private TradeType[] tradeTypes;
        public TradeType[] TradeTypes
        {
            get 
            {
                if (codes == null)
                {
                    codes = Code.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                }

                if (tradeTypes == null) 
                {
                    tradeTypes = codes.Select(c => CodeToTradeType.ContainsKey(c) ? CodeToTradeType[c] : TradeType.Unknown).ToArray();
                }

                return tradeTypes;
            } 
        }

        

        public TradeOperation TradeOperation
        {
            get
            {
                return Proceeds < 0 ? TradeOperation.Buy :
                    Proceeds > 0 ? TradeOperation.Sell :
                    TradeTypes.Any(x => x == TradeType.ExpiredPosition) ? TradeOperation.Expiration :
                    TradeTypes.Any(x => x == TradeType.Assignment) ? TradeOperation.Assignment :
                    TradeOperation.Other;
            }
        }

        public TradeType TradeType
        {
            get
            {
                if (string.IsNullOrEmpty(Code))
                {
                    return TradeType.Unknown;
                }

                return TradeTypes.Any(x => x == TradeType.Open) ? TradeType.Open :
                             TradeTypes.Any(x => x == TradeType.Close) ? TradeType.Close : 
                             TradeType.Unknown;
            }
        }

    }
}

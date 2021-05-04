using System;
using System.ComponentModel;
using Txc.Model.Trades;

namespace Txc.Model.ForexTrades
{

    public class ConvertedForexTrade: ConvertedEntity<ForexTrade>, ITradeOperation
    {
        public ConvertedForexTrade(ConvertedEntity<ForexTrade> convertedTrade)
        {
            Entity = convertedTrade.Entity;
            ConvertCurrency = convertedTrade.ConvertCurrency;
            ExchangeRate = convertedTrade.ExchangeRate;
        }

        public ConvertedForexTrade(ForexTrade entity, string convertCurrency, decimal exchangeRate): 
            base(entity, convertCurrency, exchangeRate)
        {
        }

        public TradeOperation TradeOperation { get => Entity.TradeOperation; set => Entity.TradeOperation = value; }
        public TradeType TradeType { get => Entity.TradeType; set => Entity.TradeType = value; }
        public AssetCategoryType AssetCategoryType { get => Entity.AssetCategoryType; set => Entity.AssetCategoryType = value; }
    }
}

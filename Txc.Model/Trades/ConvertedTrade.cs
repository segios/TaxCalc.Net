using System;
using System.ComponentModel;

namespace Txc.Model.Trades
{

    public class ConvertedTrade: ConvertedEntity<Trade>, ITradeOperation
    {
        public ConvertedTrade(ConvertedEntity<Trade> convertedTrade)
        {
            Entity = convertedTrade.Entity;
            ConvertCurrency = convertedTrade.ConvertCurrency;
            ExchangeRate = convertedTrade.ExchangeRate;
        }

        public ConvertedTrade(Trade entity, string convertCurrency, decimal exchangeRate): 
            base(entity, convertCurrency, exchangeRate)
        {
        }

        public TradeOperation TradeOperation { get => Entity.TradeOperation; set => Entity.TradeOperation = value; }
        public TradeType TradeType { get => Entity.TradeType; set => Entity.TradeType = value; }
        public AssetCategoryType AssetCategoryType { get => Entity.AssetCategoryType; set => Entity.AssetCategoryType = value; }
    }
}

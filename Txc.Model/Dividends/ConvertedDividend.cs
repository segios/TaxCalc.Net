using System;
using System.ComponentModel;

namespace Txc.Model.Dividends
{

    public class ConvertedDividend : ConvertedEntity<Dividend>
    {
        public ConvertedDividend(ConvertedEntity<Dividend> convertedTrade)
        {
            Entity = convertedTrade.Entity;
            ConvertCurrency = convertedTrade.ConvertCurrency;
            ExchangeRate = convertedTrade.ExchangeRate;
        }

        public ConvertedDividend(Dividend entity, string convertCurrency, decimal exchangeRate): 
            base(entity, convertCurrency, exchangeRate)
        {
        }

    }
}

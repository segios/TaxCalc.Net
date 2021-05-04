using System;
using System.ComponentModel;

namespace Txc.Model.Interests
{

    public class ConvertedInterest : ConvertedEntity<Interest>
    {
        public ConvertedInterest(ConvertedEntity<Interest> convertedTrade)
        {
            Entity = convertedTrade.Entity;
            ConvertCurrency = convertedTrade.ConvertCurrency;
            ExchangeRate = convertedTrade.ExchangeRate;
        }

        public ConvertedInterest(Interest entity, string convertCurrency, decimal exchangeRate): 
            base(entity, convertCurrency, exchangeRate)
        {
        }

    }
}

using System;
using System.ComponentModel;

namespace Txc.Model.SecuritiesLent
{

    public class ConvertedSecuritiesLentInterest : ConvertedEntity<SecuritiesLentInterest>
    {
        public ConvertedSecuritiesLentInterest(ConvertedEntity<SecuritiesLentInterest> convertedTrade)
        {
            Entity = convertedTrade.Entity;
            ConvertCurrency = convertedTrade.ConvertCurrency;
            ExchangeRate = convertedTrade.ExchangeRate;
        }

        public ConvertedSecuritiesLentInterest(SecuritiesLentInterest entity, string convertCurrency, decimal exchangeRate): 
            base(entity, convertCurrency, exchangeRate)
        {
        }

    }
}

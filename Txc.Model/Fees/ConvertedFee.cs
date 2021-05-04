using System;
using System.ComponentModel;

namespace Txc.Model.Fees
{

    public class ConvertedFee : ConvertedEntity<Fee>
    {
        public ConvertedFee(ConvertedEntity<Fee> convertedTrade)
        {
            Entity = convertedTrade.Entity;
            ConvertCurrency = convertedTrade.ConvertCurrency;
            ExchangeRate = convertedTrade.ExchangeRate;
        }

        public ConvertedFee(Fee entity, string convertCurrency, decimal exchangeRate): 
            base(entity, convertCurrency, exchangeRate)
        {
        }

    }
}

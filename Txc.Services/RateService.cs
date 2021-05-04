using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using Txc.Model;

namespace Txc.Services
{
    public class RateService : IRateService
    {
        private ConcurrentDictionary<string, ConcurrentDictionary<DateTime, decimal>> exchangeRates = 
            new ConcurrentDictionary<string, ConcurrentDictionary<DateTime, decimal>>(4, 13);
        private readonly IRatesProvider ratesProvider;

        public RateService(IRatesProvider ratesProvider)
        {
            this.ratesProvider = ratesProvider;
        }

        private string GetKey(string baseCurrency, string currency, int year) => $"{year}_{baseCurrency}{currency}";

        public decimal? GetRate(string baseCurrency, string currency, DateTime date) 
        {
            string key = GetKey(baseCurrency, currency, date.Year);

            if (!exchangeRates.ContainsKey(key)) 
            {
                LoadRates(baseCurrency, currency, date.Year);
            }
            
            if (!exchangeRates.ContainsKey(key) || !exchangeRates[key].ContainsKey(date.Date))
            {
                return null;
            }

            return exchangeRates[key][date.Date];
        }

        private void LoadRates(string baseCurrency, string currency, int year)
        {
            string key = GetKey(baseCurrency, currency, year);

            var rates = ratesProvider.GetRates(baseCurrency, currency, year);
            
            if (rates == null)
                return;

            InitCache(key, rates);
        }

        private void InitCache(string key, Rates rates)
        {
            exchangeRates[key] = new ConcurrentDictionary<DateTime, decimal>(4, 366);
            foreach (var rate in rates.Data) {
                exchangeRates[key][rate.Date] = rate.Rate;
            }
        }

    }
}

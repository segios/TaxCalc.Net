using System.IO;
using Txc.Model;

namespace Txc.Services
{
    public class FileRatesProvider: IRatesProvider
    {
        protected string CurrenciesBaseFolder { get; }
        const string ratesFolder = "Rates";

        public FileRatesProvider(string currenciesBaseFolder)
        {
            CurrenciesBaseFolder = currenciesBaseFolder;
        }

        private string GetKey(string baseCurrency, string currency, int year) => $"{year}_{baseCurrency}{currency}";

        public Rates GetRates(string baseCurrency, string currency, int year) 
        {
            string key = GetKey(baseCurrency, currency, year);

            var file = $"{key}.json";
            var filePath = Path.Combine(CurrenciesBaseFolder, ratesFolder, currency, file);

            if (!File.Exists(filePath))
            {
                return null;
            }

            var json = File.ReadAllText(filePath);
            var rates = Newtonsoft.Json.JsonConvert.DeserializeObject<Rates>(json);
            return rates;
        }
    }
}

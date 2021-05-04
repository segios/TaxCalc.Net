using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Txc.Model.Account;

namespace Txc.Model.Dividends
{
    public class DividendFormatMapping : FormatMapping<ConvertedDividend>
    {
        public DividendFormatMapping()
        {
            var defaultOptions = new FormatOptions(false, false);

            AddKey(("Symbol", null, (x) => x.Symbol, defaultOptions));
            AddKey(("SymbolName", null, (x) => string.IsNullOrEmpty(x.Entity.SymbolName) ? string.Empty : $"{x.Symbol} ({x.Entity.SymbolName})", defaultOptions));
            AddKey(("Date", null, (x) => x.OperationDate, defaultOptions));
            AddKey(("Currency", null, (x) => x.Entity.Currency, defaultOptions));
            AddKey(("GrossAmount", null, (x) => Math.Abs(x.Entity.GrossAmount), defaultOptions));
            AddKey(("Tax", null, (x) => x.Entity.Tax, defaultOptions));
            AddKey(("NetAmmount", null, (x) => x.Entity.BasisAmount, defaultOptions));

            AddKey(("LocalCurrency Rate", null, (x) => x.ExchangeRate, defaultOptions));

            var args = new Func<Profile, string>[] {
                (x) => x.TaxCurrency
            };
            AddKey(("ConvertedTax", args, (x) => Math.Round(x.Entity.Tax * x.ExchangeRate, 2), defaultOptions));
            AddKey(("ConvertedNetAmmount", args, (x) => x.ConvertedBasis, defaultOptions));
            
            AddTotalKey(("ConvertedTax", (x) => Math.Round(x.Entity.Tax * x.ExchangeRate, 2)));
            AddTotalKey(("ConvertedNetAmmount", (x) => x.ConvertedBasis));
        }
    }
}

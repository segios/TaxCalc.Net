using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Txc.Model.Account;

namespace Txc.Model.SecuritiesLent
{
    public class SecuritiesLentInterestFormatMapping : FormatMapping<ConvertedSecuritiesLentInterest>
    {
        public SecuritiesLentInterestFormatMapping()
        {
            var defaultOptions = new FormatOptions(false, false);

            AddKey(("Symbol", null, (x) => x.Symbol, defaultOptions));
            AddKey(("Date", null, (x) => x.OperationDate, defaultOptions));
            AddKey(("Currency", null, (x) => x.Entity.Currency, defaultOptions));
            AddKey(("Interest Paid", null, (x) => x.BasisAmount, defaultOptions));

            AddKey(("LocalCurrency Rate", null, (x) => x.ExchangeRate, defaultOptions));

            var args = new Func<Profile, string>[] {
                (x) => x.TaxCurrency
            };

            AddKey(("ConvertedInterestPaid", args, (x) => x.ConvertedBasis, defaultOptions));

            AddTotalKey(("ConvertedInterestPaid", (x) => x.ConvertedBasis));
        }
    }
}

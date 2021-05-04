using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Txc.Model.Account;

namespace Txc.Model.Fees
{
    public class FeeFormatMapping : FormatMapping<ConvertedFee>
    {
        public FeeFormatMapping()
        {
            var defaultOptions = new FormatOptions(false, false);

            AddKey(("Description", null, (x) => x.Entity.Description, new FormatOptions(false, false, 60)));
            AddKey(("Date", null, (x) => x.OperationDate, defaultOptions));
            AddKey(("Currency", null, (x) => x.Entity.Currency, defaultOptions));
            AddKey(("Amount", null, (x) => x.BasisAmount, defaultOptions));

            AddKey(("LocalCurrency Rate", null, (x) => x.ExchangeRate, defaultOptions));

            var args = new Func<Profile, string>[] {
                (x) => x.TaxCurrency
            };

            AddKey(("ConvertedAmount", args, (x) => x.ConvertedBasis, defaultOptions));

            AddTotalKey(("ConvertedAmount", (x) => x.ConvertedBasis));
        }
    }
}

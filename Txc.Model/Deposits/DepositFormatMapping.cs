using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Txc.Model.Account;

namespace Txc.Model.Deposits
{
    public class DepositFormatMapping : FormatMapping<Deposit>
    {
        public DepositFormatMapping()
        {
            var defaultOptions = new FormatOptions(false, false);

            AddKey(("Description", null, (x) => x.Description, new FormatOptions(false, false, 60)));
            AddKey(("Date", null, (x) => x.Date, defaultOptions));
            AddKey(("Currency", null, (x) => x.Currency, defaultOptions));
            AddKey(("Amount", null, (x) => x.Amount, defaultOptions));

            var args = new Func<Profile, string>[] {
                (x) => x.TaxCurrency
            };

            AddKey(("BankComm", args, (x) => 0, defaultOptions));
          //  AddKey(("LocalCurrency Rate", null, (x) => x.ExchangeRate, defaultOptions));

            //    AddKey(("ConvertedComm", null, (x) => 0, defaultOptions));

            //     AddTotalKey(("ConvertedAmount", (x) => x.ConvertedBasis));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Txc.Model.Account;

namespace Txc.Model.Trades
{
    public class TradeFormatMapping : FormatMapping<ConvertedTrade>//, IFormatMapping<Trade>
    {
        public TradeFormatMapping()
        {
            var defaultOptions = new FormatOptions(false, true);
            var localizableOptions = new FormatOptions(true, false);

            AddKey(("Category", null, (x) => x.Entity.AssetCategoryType, localizableOptions));
            AddKey(("Symbol", null, (x) => x.Entity.Symbol, defaultOptions));
            
            AddKey(("Date/Time", null, (x) => x.Entity.DateTime, defaultOptions));
            AddKey(("Currency", null, (x) => x.Entity.Currency, defaultOptions));
            AddKey(("Quantity", null, (x) => x.Entity.Quantity, defaultOptions));
            AddKey(("T. Price", null, (x) => x.Entity.TPrice, defaultOptions));
            AddKey(("Proceeds", null, (x) => x.Entity.Proceeds, defaultOptions));
            AddKey(("Comm/Fee", null, (x) => x.Entity.Comm, defaultOptions));

            AddKey(("Basis", null, (x) => x.Entity.BasisAmount, defaultOptions));
            AddKey(("LocalCurrency Rate", null, (x) => x.ExchangeRate, defaultOptions));
            
            var args = new Func<Profile, string>[] {
                (x) => x.TaxCurrency
            };

            AddKey(("ConvertedProceeds", args, (x) => x.ConvertedGrossAmount, defaultOptions));
            AddKey(("ConvertedComm", args, (x) => x.ConvertedComm, defaultOptions));

            AddKey(("LocalCurrency Basis", args, (x) => x.ConvertedBasis, defaultOptions));
            AddKey(("Operation", null, (x) => x.Entity.TradeOperation, localizableOptions));
            AddKey(("Type", null, (x) => x.Entity.TradeType, localizableOptions));

            AddTotalKey(("LocalCurrency Basis", (x) => x.ConvertedBasis));
        }
    }
}

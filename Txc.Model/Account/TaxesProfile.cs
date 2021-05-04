using System;
using System.Collections.Generic;
using System.Text;

namespace Txc.Model.Account
{
    public class TaxesProfile
    {
        public TaxClassCodes TaxClassCode { get; set; }
        public List<Tax> Taxes { get; set; }

        public IEnumerable<(Tax tax, decimal taxValue)> ApplyTaxes(decimal value)
        {
            foreach (var tax in Taxes)
            {
                yield return (tax, tax.ApplyTax(value));
            }
        }

        public static TaxesProfile RegularTaxProfile()
        {
            return new TaxesProfile()
            {
                TaxClassCode = TaxClassCodes.General,
                Taxes = new List<Tax>() {
                    new Tax("pdfo", "ПДФО",  18),
                    new Tax("viyzbir", "Військовий збір",  1.5M),
                }
            };
        }

        public static TaxesProfile DividendsTaxProfile()
        {
            return new TaxesProfile()
            {
                TaxClassCode = TaxClassCodes.General,
                Taxes = new List<Tax>() {
                    new Tax("pdfo", "ПДФО",  9),
                    new Tax("viyzbir", "Військовий збір",  1.5M),
                }
            };
        }
    }


}

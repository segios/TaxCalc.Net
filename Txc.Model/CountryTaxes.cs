using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Txc.Model.Account;

namespace Txc.Model
{
    public class CountryTaxes
    {
        public string CountryCode { get; set; }

        public Dictionary<TaxClassCodes, TaxesProfile> TaxesProfiles { get; set; }  = new Dictionary<TaxClassCodes, TaxesProfile>();
    }
}

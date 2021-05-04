using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Txc.Model.Account
{
    public class Profile
    {
        public string TaxCurrency { get; set; }
        
        public string CultureCode { get; set; }
        public string CountryCode { get; set; }
        public string LocalCurrency { get; set; }
        public Dictionary<TaxClassCodes, TaxesProfile> TaxesProfiles { get; set; }
        public List<byte[]> Statements { get; set; } = new List<byte[]>();
    }

}

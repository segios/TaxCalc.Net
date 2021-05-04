using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Txc.Model.Account;

namespace Txc.Model
{
    public class ReportOptions
    {
        public BrokerCode BrokerCode { get; set; } = BrokerCode.IB;

        public int Year { get; set; } = DateTime.Now.Year - 1;
        public CalcMethod CalcMethod { get; set; } = CalcMethod.FIFO;
        public string BaseCurrency { get; set; } = "USD";
        public Profile Profile { get; set; }

        public EntityCode ReportEntitities { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Txc.Model;
using Txc.Model.Trades;

namespace Txc.Model.Fees
{

    public class Fee : IRateConvertable
    {
      
        public string Currency { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }

        public string Symbol => string.Empty;

        public decimal BasisAmount => Amount;
        public DateTime OperationDate => Date;

        decimal IRateConvertable.GrossAmount => Amount;
        decimal IRateConvertable.Comm => 0;
    }
}

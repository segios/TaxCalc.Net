using System;
using System.Collections.Generic;
using System.Linq;
using Txc.Model;
using Txc.Model.Trades;

namespace Txc.Model.Deposits
{
    //Currency,Settle Date, Description, Amount
    public class Deposit : IOperation
    {
      
        public string Currency { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }

        public DateTime OperationDate => Date;
    }
}

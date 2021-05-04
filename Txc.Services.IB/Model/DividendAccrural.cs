using System;
using System.Collections.Generic;
using System.Linq;
using Txc.Model;

namespace Txc.Services.IB.Model
{

    public class DividendAccrural
    {
        private static Dictionary<string, FinancialOperation> CodeToOperation = new Dictionary<string, FinancialOperation>()
        {
            { "Po", FinancialOperation.Posting },
            { "Re", FinancialOperation.Reversal },
        };

        public string AssetCategory { get; set; }
        public string Currency { get; set; }
        public string Symbol { get; set; }
        public DateTime Date { get; set; }
        public DateTime ExDate { get; set; }
        public DateTime PayDate { get; set; }
        public decimal Quantity { get; set; }
        public decimal Tax { get; set; }
        public decimal Fee { get; set; }
        public decimal GrossRate { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal NetAmount { get; set; }
        public string Code { get; set; }

        public FinancialOperation Operation
        {
            get
            {
                if (Code != null && CodeToOperation.ContainsKey(Code)) 
                {
                    return CodeToOperation[Code];
                }

                return FinancialOperation.Unknown;
            }
        }

    }
}

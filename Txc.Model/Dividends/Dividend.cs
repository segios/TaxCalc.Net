using System;
using System.Collections.Generic;
using System.Linq;
using Txc.Model;

namespace Txc.Model.Dividends
{

    public class Dividend: IRateConvertable
    {
        public string AssetCategory { get; set; }
        public string Currency { get; set; }
        public string Symbol { get; set; }
        public string SymbolName { get; set; }
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
      
        public decimal BasisAmount => Math.Abs(NetAmount);

        string IRateConvertable.Currency => this.Currency;
        decimal IRateConvertable.GrossAmount => this.GrossAmount;
        decimal IRateConvertable.Comm => this.Tax;

        public DateTime OperationDate => this.PayDate;
    }
}

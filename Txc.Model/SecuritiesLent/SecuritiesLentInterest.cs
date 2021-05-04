using System;
using Txc.Model;

namespace Txc.Model.SecuritiesLent
{
    //Currency,Value Date,Symbol,Start Date,Quantity,Collateral Amount,Interest Rate Earned by IB (%),
    //Interest Paid to IB,Interest Rate on Customer Collateral (%),Interest Paid to Customer,Code
    public class SecuritiesLentInterest : IRateConvertable 
    {
        public string Currency { get; set; }
        public DateTime ValueDate { get; set; }
        public string Symbol { get; set; }
        public DateTime StartDate { get; set; }
        public decimal Quantity { get; set; }
        public decimal CollateralAmount { get; set; }
        public decimal IBInterestRate { get; set; }
        public decimal IBPaidInterest { get; set; }
        public decimal CustomerInterestRate { get; set; }
        public decimal CustomerPaidInterest { get; set; }
        public string Code { get; set; }

        public decimal BasisAmount => IBPaidInterest;

        string IRateConvertable.Currency => this.Currency;

        public DateTime OperationDate => this.ValueDate;

        decimal IRateConvertable.GrossAmount => IBPaidInterest;
        decimal IRateConvertable.Comm => 0;
    }
}

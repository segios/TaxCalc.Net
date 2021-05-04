using System;
using Txc.Model;

namespace Txc.Services.IB.Model
{
    //Currency,Value Date,Symbol,Start Date,Quantity,Collateral Amount,Interest Rate Earned by IB (%),
    //Interest Paid to IB,Interest Rate on Customer Collateral (%),Interest Paid to Customer,Code
    public class IBSecuritiesLentInterest  
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
        public FinancialOperation Operation
        {
            get
            {
                if (string.IsNullOrEmpty(Code))
                {
                    return FinancialOperation.Unknown;
                }

                return Code.Contains("Po") ? FinancialOperation.Posting :
                             Code.Contains("Re") ? FinancialOperation.Reversal : FinancialOperation.Unknown;
            }
        }

    }
}

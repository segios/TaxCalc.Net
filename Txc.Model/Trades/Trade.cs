using System;
using System.ComponentModel;

namespace Txc.Model.Trades
{

    public class Trade: IRateConvertable, IValidable, ITradeOperation
    {
        [Localizable(true)]
        public AssetCategoryType AssetCategoryType { get; set; }
        public string Currency { get; set; }
        public string Symbol { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Quantity { get; set; }
        public decimal TPrice { get; set; }
        public decimal Proceeds { get; set; }

        //private decimal comm;
        //// consider commission should be always negative
        //public decimal Comm { get => comm; set => comm = -1 * Math.Abs(value); }
        public decimal Comm { get ; set ; }
        public decimal Basis { get; set; }
        public decimal RealizedPL { get; set; }
        public string Code { get; set; }

        [Localizable(true)]
        public TradeOperation TradeOperation { get; set; }
        
        [Localizable(true)]
        public TradeType TradeType { get; set; }

        public decimal BasisAmount 
        {
            get 
            {
                return Proceeds + Comm;
            } 
        }

        decimal IRateConvertable.GrossAmount => Proceeds;
        
        decimal IRateConvertable.Comm => Comm;

        string IRateConvertable.Currency => this.Currency;

        DateTime IOperation.OperationDate => this.DateTime;

        public Trade Split(decimal? qnty = null) 
        {
            var res = (Trade)this.MemberwiseClone();
            if (!qnty.HasValue)
            {
                return res;
            }

            var mult = AssetCategoryType == AssetCategoryType.Options ? 100 : 1;
            var sign = (TradeOperation == TradeOperation.Sell ? -1 : 1);

            res.Quantity = Math.Abs(qnty.Value) * sign;
            var ratio = Math.Abs(qnty.Value) / Math.Abs(this.Quantity);
            res.Comm = res.Comm * ratio;
            res.Proceeds = -1 * res.Quantity * res.TPrice * mult;

            // update current trade
            Quantity = sign * (Math.Abs(this.Quantity) - Math.Abs(qnty.Value));
            Comm = Comm - res.Comm;
            Proceeds = -1 * Quantity * TPrice * mult;

            return res;
        }

        // IValidable
        public bool IsValid { get; set; } = true;
        public string Error { get; set; }
    }
}

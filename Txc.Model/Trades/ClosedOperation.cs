using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Txc.Model.Trades
{

    public class ClosedOperation<T> : IClosedOperation 
        where T: IRateConvertable, IValidable, ITradeOperation
    {
        public List<T> OpenTrades { get; set; } = new List<T>();
        public T CloseTrade { get; set; }
        
        public string Symbol { get { return CloseTrade?.Symbol; } }

        public DateTime? Date
        { 
            get{
                return CloseTrade?.OperationDate;
            } 
        }

        public bool NoOpenTrade
        {
            get
            {
                return !OpenTrades.Any();
            }
        }

        public bool IsClosed
        {
            get
            {
                return CloseTrade != null;
            }
        }

        public void Validate() 
        {
            if (NoOpenTrade && IsClosed && 
                (CloseTrade.TradeOperation == TradeOperation.Buy || CloseTrade.TradeOperation == TradeOperation.Sell)) 
            {
                CloseTrade.IsValid = false;
                CloseTrade.Error = "Open trades not found";
            }
        }
    }
}

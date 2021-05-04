using System;
using System.Collections;
using System.Collections.Generic;
using Txc.Model.Trades;

namespace Txc.Services
{
    public class TradeTransformer :  IDataTransformer<Trade, ClosedOperation<Trade>>
    {
        public IList<ClosedOperation<Trade>> Transform(IList<Trade> entities) 
        {
            List<ClosedOperation<Trade>> res = new List<ClosedOperation<Trade>>();

            Queue<Trade> queue = new Queue<Trade>();

            var currentAsset = "";
            foreach (var trade in entities)
            {
                if (currentAsset != trade.Symbol)
                {
                    queue.Clear();
                    currentAsset = trade.Symbol;

                    if (trade.TradeType == TradeType.Open)
                    {
                        queue.Enqueue(trade);
                        continue;
                    }
                    else if (trade.TradeType == TradeType.Close)
                    {
                        var closedOperation = new ClosedOperation<Trade>();
                        closedOperation.CloseTrade = trade;
                        res.Add(closedOperation);
                        continue;
                    }
                }

                if (trade.TradeType == TradeType.Open)
                {
                    queue.Enqueue(trade);
                }
                else if (trade.TradeType == TradeType.Close)
                {
                    var closedOperation = new ClosedOperation<Trade>();

                    var closedAmount = Math.Abs(trade.Quantity);
                    while (closedAmount > 0 && queue.Count > 0)
                    {
                        var oTrade = queue.Peek();
                        if (Math.Abs(oTrade.Quantity) <= closedAmount)
                        {
                            oTrade = queue.Dequeue();
                            closedOperation.OpenTrades.Add(oTrade);
                            closedAmount -= Math.Abs(oTrade.Quantity);
                        }
                        else
                        {
                            // split trade 
                            var newTrade = oTrade.Split(closedAmount);
                            closedOperation.OpenTrades.Add(newTrade);
                            
                            closedAmount -= Math.Abs(newTrade.Quantity);
                        }
                    }

                    closedOperation.CloseTrade = trade;
                    res.Add(closedOperation);
                }
            }

            return res;

        }

    }
}

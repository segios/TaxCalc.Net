using System;
using System.Collections;
using System.Collections.Generic;
using Txc.Model.ForexTrades;
using Txc.Model.Trades;

namespace Txc.Services
{
    public class ForexTradeTransformer : IDataTransformer<ForexTrade, ClosedOperation<ForexTrade>>
    {
        public IList<ClosedOperation<ForexTrade>> Transform(IList<ForexTrade> entities)
        {
            List<ClosedOperation<ForexTrade>> res = new List<ClosedOperation<ForexTrade>>();

            Queue<ForexTrade> queue = new Queue<ForexTrade>();

            var currentAsset = "";
            foreach (var trade in entities)
            {
                if (currentAsset != trade.Symbol)
                {
                    queue.Clear();
                    currentAsset = trade.Symbol;
                }

                if (queue.Count > 0)
                {
                    var oTrade = queue.Peek();
                    if (oTrade.TradeOperation == trade.TradeOperation)
                    {
                        queue.Enqueue(trade);
                        continue;
                    }

                    var closedOperation = new ClosedOperation<ForexTrade>();
                    res.Add(closedOperation);

                    var closedAmount = Math.Abs(trade.Quantity);

                    while (closedAmount > 0 && queue.Count > 0)
                    {
                        oTrade = queue.Peek();
                        if (Math.Abs(oTrade.Quantity) <= closedAmount)
                        {
                            oTrade = queue.Dequeue();
                            closedOperation.OpenTrades.Add(oTrade);
                            closedAmount -= Math.Abs(oTrade.Quantity);
                        }
                        else
                        {
                            // split trade 
                            var newTrade = (ForexTrade)oTrade.Split(closedAmount);
                            closedOperation.OpenTrades.Add(newTrade);

                            closedAmount -= Math.Abs(newTrade.Quantity);
                        }
                    }

                    if (closedAmount == 0)
                    {
                        closedOperation.CloseTrade = trade;
                    }
                    else
                    {
                        var restClose = (ForexTrade)trade.Split(closedAmount);
                        closedOperation.CloseTrade = trade;
                        queue.Enqueue(restClose);
                    }
                }
                else 
                {
                    queue.Enqueue(trade);
                }

            }

            return res;

        }

    }
}

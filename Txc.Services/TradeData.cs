using System.Collections.Generic;
using System.Linq;
using Txc.Model.Trades;

namespace Txc.Services
{
    public class TradeData
    {
        public IList<ClosedOperation<ConvertedTrade>> Trades { get; set; } = new List<ClosedOperation<ConvertedTrade>>();
        public IList<TradeAggregation> TradeAggregations { get; set; } = new List<TradeAggregation>();

        public IEnumerable<ConvertedTrade> FlatConvertedTrades
        {
            get 
            {
                foreach (var ct in Trades)
                {
                    if (ct.OpenTrades.Any())
                    {
                        foreach(var ot in ct.OpenTrades) 
                        {
                            yield return ot;
                        }
                        
                    }

                    if (ct.IsClosed)
                    {
                        yield return ct.CloseTrade;
                    }
                }
            }
        }
    }
}

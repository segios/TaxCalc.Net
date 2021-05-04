using Txc.Model.Trades;

namespace Txc.Model
{
    public interface ITradeOperation
    {
        TradeOperation TradeOperation { get; set; }
        TradeType TradeType { get; set; }
        AssetCategoryType AssetCategoryType { get; set; }
    }

}

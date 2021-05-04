using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Txc.Model;
using Txc.Model.Trades;
using Txc.Services.IB.Model;
using Trade = Txc.Services.IB.Model.IBForexTrade;

namespace Txc.Services.IB.Mappings
{
    // AssetCategory, Currency Symbol  Date/Time Quantity    T.Price Proceeds    Comm/Fee Basis   Realized P/L Code

    public class ForexTradeMapping : ClassMapping<Trade>
    {
        public ForexTradeMapping()
        {
            MapSection("Trades");

            Map("Asset Category", nameof(Trade.AssetCategory));
            Map("Currency", nameof(Trade.Currency));
            Map("Symbol", nameof(Trade.Symbol));
            Map("Date/Time", nameof(Trade.DateTime));
            Map("Quantity", nameof(Trade.Quantity));
            Map("T. Price", nameof(Trade.TPrice));
            Map("Proceeds", nameof(Trade.Proceeds));
            Map("Comm/Fee", nameof(Trade.Comm));
            Map("Basis", nameof(Trade.Basis));
            Map("Realized P/L", nameof(Trade.RealizedPL));
            Map("Code", nameof(Trade.Code));

            SetFilter(x => ((Trade)x).AssetCategoryType == AssetCategoryType.Forex);
        }
    }
}

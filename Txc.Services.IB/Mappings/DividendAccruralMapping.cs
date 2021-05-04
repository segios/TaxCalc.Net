using Txc.Model;
using Txc.Model.Trades;
using Txc.Services.IB.Model;

namespace Txc.Services.IB.Mappings
{
    //Asset Category Currency    Symbol Date    Ex Date Pay Date    Quantity Tax Fee Gross Rate Gross Amount Net Amount Code

    public class DividendAccruralMapping : ClassMapping<DividendAccrural>
    {
        public DividendAccruralMapping()
        {
            MapSection("Change in Dividend Accruals");

            Map("Asset Category", nameof(DividendAccrural.AssetCategory));
            Map("Currency", nameof(DividendAccrural.Currency));
            Map("Symbol", nameof(DividendAccrural.Symbol));
            Map("Date", nameof(DividendAccrural.Date));
            Map("Ex Date", nameof(DividendAccrural.ExDate));
            Map("Pay Date", nameof(DividendAccrural.PayDate));
            Map("Quantity", nameof(DividendAccrural.Quantity));
            Map("Tax", nameof(DividendAccrural.Tax));
            Map("Fee", nameof(DividendAccrural.Fee));
            Map("Gross Rate", nameof(DividendAccrural.GrossRate));
            Map("Gross Amount", nameof(DividendAccrural.GrossAmount));
            Map("Net Amount", nameof(DividendAccrural.NetAmount));
            Map("Code", nameof(DividendAccrural.Code));

            SetFilter(x => ((DividendAccrural)x).AssetCategory == AssetCategoryType.Stocks.ToString() &&
                            ((DividendAccrural)x).Operation == FinancialOperation.Reversal);
        }
    }
}

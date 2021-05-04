using Txc.Model;
using Txc.Services.IB.Model;
using SecuritiesLentInterest = Txc.Services.IB.Model.IBSecuritiesLentInterest;

namespace Txc.Services.IB.Mappings
{
    public class SecuritiesLentInterestMapping : ClassMapping<SecuritiesLentInterest>
    {
        public SecuritiesLentInterestMapping()
        {
            MapSection("IBKR Managed Securities Lent Interest Details (Stock Yield Enhancement Program)");

            Map("Currency", nameof(SecuritiesLentInterest.Currency));
            Map("Value Date", nameof(SecuritiesLentInterest.ValueDate));
            Map("Symbol", nameof(SecuritiesLentInterest.Symbol));
            Map("Start Date", nameof(SecuritiesLentInterest.StartDate));
            Map("Quantity", nameof(SecuritiesLentInterest.Quantity));
            Map("Collateral Amount", nameof(SecuritiesLentInterest.CollateralAmount));
            Map("Interest Rate Earned by IB (%)", nameof(SecuritiesLentInterest.IBInterestRate));
            Map("Interest Paid to IB", nameof(SecuritiesLentInterest.IBPaidInterest));
            Map("Interest Rate on Customer Collateral (%)", nameof(SecuritiesLentInterest.CustomerInterestRate));
            Map("Interest Paid to Customer", nameof(SecuritiesLentInterest.CustomerPaidInterest));
            Map("Code", nameof(SecuritiesLentInterest.Code));
        }
    }
}

using Txc.Model;
using Txc.Services.IB.Model;


namespace Txc.Services.IB.Mappings
{
    //Deposits & Withdrawals,Header,Currency,Settle Date, Description, Amount
    public class DepositMapping : ClassMapping<IBDeposit>
    {
        public DepositMapping()
        {
            MapSection("Deposits & Withdrawals");

            Map("Currency", nameof(IBDeposit.Currency));
            Map("Settle Date", nameof(IBDeposit.Date));
            Map("Description", nameof(IBDeposit.Description));
            Map("Amount", nameof(IBDeposit.Amount));

            SetFilter(x => ((IBDeposit)x).Amount > 0 && ((IBDeposit)x).Currency != "Total");
        }
    }
}

using Txc.Model;
using Txc.Services.IB.Model;


namespace Txc.Services.IB.Mappings
{
    //Interest,Header,Currency,Date,Description,Amount
    public class FeeMapping : ClassMapping<IBFee>
    {
        public FeeMapping()
        {
            MapSection("Fees");

            Map("Currency", nameof(IBFee.Currency));
            Map("Date", nameof(IBFee.Date));
            Map("Description", nameof(IBFee.Description));
            Map("Amount", nameof(IBFee.Amount));
            
        }
    }
}

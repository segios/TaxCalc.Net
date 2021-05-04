using Txc.Model;
using Txc.Services.IB.Model;


namespace Txc.Services.IB.Mappings
{
    //Interest,Header,Currency,Date,Description,Amount
    public class InterestMapping : ClassMapping<IBInterest>
    {
        public InterestMapping()
        {
            MapSection("Interest");

            Map("Currency", nameof(IBInterest.Currency));
            Map("Date", nameof(IBInterest.Date));
            Map("Description", nameof(IBInterest.Description));
            Map("Amount", nameof(IBInterest.Amount));
            
        }
    }
}

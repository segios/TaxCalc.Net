using Txc.Model;
using Txc.Services.IB.Model;


namespace Txc.Services.IB.Mappings
{
    //Asset Category, Symbol, Description, Conid, Security ID,Listing Exch, Multiplier, Type, Code
    public class FinInstrumentMapping : ClassMapping<IBFinInstrument>
    {
        public FinInstrumentMapping()
        {
            MapSection("Financial Instrument Information");

            Map("Asset Category", nameof(IBFinInstrument.AssetCategory));
            Map("Symbol", nameof(IBFinInstrument.Symbol));
            Map("Description", nameof(IBFinInstrument.Description));
            Map("Conid", nameof(IBFinInstrument.Conid));
            Map("Security ID", nameof(IBFinInstrument.SecurityID));
            Map("Listing Exch", nameof(IBFinInstrument.ListingExch));
            Map("Multiplier", nameof(IBFinInstrument.Multiplier));

            Map("Expiry", nameof(IBFinInstrument.Expiry));
            Map("Delivery Month", nameof(IBFinInstrument.DeliveryMonth));

            Map("Type", nameof(IBFinInstrument.Type));
            Map("Strike", nameof(IBFinInstrument.Strike));
            Map("Code", nameof(IBFinInstrument.Code));
        }
    }
}

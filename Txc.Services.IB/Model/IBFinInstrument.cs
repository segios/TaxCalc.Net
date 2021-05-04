using System;

namespace Txc.Services.IB.Model
{

//    Asset Category, Symbol, Description, Conid, Security ID,Listing Exch, Multiplier, Type, Code

    public class IBFinInstrument
    {
        public string AssetCategory { get; set; }
        public string Symbol { get; set; }
        public string Description { get; set; }
        public string Conid { get; set; }
        public string SecurityID { get; set; }
        public string ListingExch { get; set; }
        public decimal Multiplier { get; set; }
        public DateTime Expiry { get; set; }
        public string DeliveryMonth { get; set; }
        public string Type { get; set; }
        public decimal Strike { get; set; }

        public string Code { get; set; }

    }
}

using Txc.Model;
using Txc.Services.IB.Model;


namespace Txc.Services.IB.Mappings
{
    //Asset Category, Symbol, Description, Conid, Security ID,Listing Exch, Multiplier, Type, Code
    public class AccountInformationMapping : ClassMapping<IBAccountInformation>
    {
        public AccountInformationMapping()
        {
            MapSection("Account Information");

            Map("Field Name", nameof(IBAccountInformation.Name));
            Map("Field Value", nameof(IBAccountInformation.Value));
        }
    }
}

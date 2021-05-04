using AutoMapper;
using Txc.Model.AccountInformations;
using Txc.Services.AutoMapperConfig;
using Txc.Services.IB.Model;

namespace Txc.Services.IB.AutoMapperConfig
{
    public class AccountInformationMap : IAutoMapperConfig
    {
        public void Map(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<IBAccountInformation, AccountInformation>();
        }
    }
}

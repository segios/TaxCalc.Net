using AutoMapper;
using Txc.Model.FinInstruments;
using Txc.Services.AutoMapperConfig;
using Txc.Services.IB.Model;

namespace Txc.Services.IB.AutoMapperConfig
{
    public class FinInstrumentMap: IAutoMapperConfig
    {
        public void Map(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<IBFinInstrument, FinInstrument>();
        }
    }
}

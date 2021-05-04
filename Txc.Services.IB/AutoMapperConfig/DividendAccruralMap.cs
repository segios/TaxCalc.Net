using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Txc.Model.Dividends;
using Txc.Model.Trades;
using Txc.Services.AutoMapperConfig;
using Txc.Services.IB.Model;

namespace Txc.Services.IB.AutoMapperConfig
{

    public class DividendAccruralMap : IAutoMapperConfig
    {
        public void  Map(IMapperConfigurationExpression cfg) {
            cfg.CreateMap<DividendAccrural, Dividend>();
        }
    }
}

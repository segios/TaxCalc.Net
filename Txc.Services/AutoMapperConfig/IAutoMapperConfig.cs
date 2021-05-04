using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Txc.Services.AutoMapperConfig
{
    public interface IAutoMapperConfig
    {
        void Map(IMapperConfigurationExpression cfg);
    }
}

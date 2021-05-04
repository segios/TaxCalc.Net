using AutoMapper;
using System;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Txc.Services.Extensions;

namespace Txc.Services.AutoMapperConfig
{
    public static class AutoMapperConfigHelper
    {
        public static void Configure(IMapperConfigurationExpression cfg, Assembly[] assemblies) 
        {
            foreach (var a in assemblies) 
            {
                foreach (var mappingType in a.GetTypesWithInterface<IAutoMapperConfig>()) 
                {
                    var mapping  = Activator.CreateInstance(mappingType) as IAutoMapperConfig;
                    mapping.Map(cfg);
                }
            }
        }

       

       
    }
}

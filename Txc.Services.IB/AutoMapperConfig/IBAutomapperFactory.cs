using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Txc.Services.IB.AutoMapperConfig
{
    public class IBAutomapperFactory
    {
        private MapperConfiguration cfg;
        public IBAutomapperFactory(IBAutomapperConfig automapperConfig)
        {
            this.cfg = automapperConfig.Config;
        }

        public IMapper GetMapper()
        {
            var mapper = cfg.CreateMapper();
            return mapper;
        }
    }
}
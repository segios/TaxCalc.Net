using AutoMapper;
using Txc.Services.AutoMapperConfig;

namespace Txc.Services.IB.AutoMapperConfig
{
    public class IBAutomapperConfig
    {
        private MapperConfiguration cfg;

        public IBAutomapperConfig()
        {
            CreateConfig();
        }

        public MapperConfiguration Config
        {
            get
            {
                if (cfg == null)
                {
                    cfg = CreateConfig();
                }
                return cfg;
            }
        }

        private MapperConfiguration CreateConfig()
        {
            var config = new MapperConfiguration(cfg =>
            {
                //cfg.AddProfile<AppProfile>();
                AutoMapperConfigHelper.Configure(cfg, new[] { typeof(TradeMap).Assembly });
            });

            return config;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Txc.Services
{

    public class BrokerParserProviderFactory : BaseFactory<IBrokerParserProvider>, IBrokerParserProviderFactory
    {
        public BrokerParserProviderFactory(IFactory<IBrokerParserProvider> concreateFactory): base(concreateFactory) 
        {
        }
    }
}
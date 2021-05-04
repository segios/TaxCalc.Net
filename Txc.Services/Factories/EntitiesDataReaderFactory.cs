using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Txc.Services
{
    
    public class EntitiesDataReaderFactory : BaseFactory<IEntitiesDataReader>, IEntitiesDataReaderFactory
    {
        public EntitiesDataReaderFactory(IFactory<IEntitiesDataReader> concreateFactory) : base(concreateFactory)
        {
        }
    }
}
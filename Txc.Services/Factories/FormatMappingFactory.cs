using System;
using Txc.Model;

namespace Txc.Services
{

    public class FormatMappingFactory : BaseTypeFactory, IFormatMappingFactory
    {
        public FormatMappingFactory(IRealTypeFactory concreateFactory) : base(concreateFactory)
        {
        }

        public TFormatMapping GetInstance<TFormatMapping, TEntity>()
            where TFormatMapping : class, IFormatMapping<TEntity>
        {
            Type type = typeof(TFormatMapping);

            return GetInstance<TFormatMapping>(type);
        }
    }
}
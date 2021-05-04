using System;
using Txc.Model;

namespace Txc.Services
{

    public class ClassMappingFactory : BaseTypeFactory, IClassMappingFactory
    {
        public ClassMappingFactory(IRealTypeFactory concreateFactory) : base(concreateFactory)
        {
        }

        public TClassMapping GetInstance<TClassMapping, TEntity>() where TClassMapping : class, IClassMapping<TEntity> 
        {
            Type type = typeof(TClassMapping);

            return GetInstance<TClassMapping>(type);
        }

    }
}
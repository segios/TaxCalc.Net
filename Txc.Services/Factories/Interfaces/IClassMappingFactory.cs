using System;
using Txc.Model;

namespace Txc.Services
{
    public interface IClassMappingFactory : ITypeFactory
    {
        TClassMapping GetInstance<TClassMapping, TEntity>() where TClassMapping : class, IClassMapping<TEntity>;

    }
}

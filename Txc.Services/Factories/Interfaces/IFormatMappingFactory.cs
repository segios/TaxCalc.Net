using Txc.Model;

namespace Txc.Services
{
    public interface IFormatMappingFactory : ITypeFactory
    {
        TFormatMapping GetInstance<TFormatMapping, TEntity>()
            where TFormatMapping : class, IFormatMapping<TEntity>;
            //where TEntity : IRateConvertable;

    }
}

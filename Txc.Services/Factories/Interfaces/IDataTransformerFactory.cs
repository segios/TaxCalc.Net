namespace Txc.Services
{
    public interface IDataTransformerFactory : ITypeFactory
    {
        TDataTransformer GetInstance<TDataTransformer, TEntity, TResult>() where TDataTransformer : class, IDataTransformer<TEntity, TResult>;
    }
}

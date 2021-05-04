
using System;

namespace Txc.Services
{

    public class DataTransformerFactory : BaseTypeFactory, IDataTransformerFactory
    {
        public DataTransformerFactory(IRealTypeFactory concreateFactory) : base(concreateFactory)
        {
        }


        public TDataTransformer GetInstance<TDataTransformer, TEntity, TResult>() where TDataTransformer : class, IDataTransformer<TEntity, TResult>
        {
            Type type = typeof(TDataTransformer);

            return this.GetInstance<TDataTransformer>(type);
        }
    }
}
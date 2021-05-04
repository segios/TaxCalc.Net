using System.Collections;
using System.Collections.Generic;

namespace Txc.Services
{

    public interface IDataTransformer<T, TResult>
    {
        IList<TResult> Transform(IList<T> entities);
    }

    public interface IDataTransformer : IDataTransformer<object, object>
    {
    }

}

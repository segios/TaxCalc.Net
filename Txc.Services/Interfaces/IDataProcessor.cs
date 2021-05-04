using System.Collections.Generic;

namespace Txc.Services
{
    public interface IDataProcessor<T>
    {
        IList<T> Process(IList<T> entities);
    }
}

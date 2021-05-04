using System.Collections.Generic;
using System.Threading.Tasks;
using Txc.Model;

namespace Txc.Services
{
    public interface IEntityConvertor<T> where T : IRateConvertable
    {
        List<ConvertedEntity<T>> Convert(string localCurrency, IEnumerable<T> entities);
        ConvertedEntity<T> Convert(string localCurrency, T entity);
    }
}

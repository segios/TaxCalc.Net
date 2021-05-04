using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Txc.Services
{
    public interface IBrokerParserProvider
    {
        IStatementParser StatementParser { get; }
        IEntitiesDataReader EntitiesDataReader { get; }
    }
}

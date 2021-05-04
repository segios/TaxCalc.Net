using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Txc.Services
{

    public class StatementParserFactory : BaseFactory<IStatementParser>, IStatementParserFactory
    {
        public StatementParserFactory(IFactory<IStatementParser> concreateFactory) : base(concreateFactory)
        {
        }
    }
}
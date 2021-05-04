using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Txc.Model;

namespace Txc.Services.IB
{

    public class IBBrokerParserProvider : IBrokerParserProvider
    {
        private readonly IEntitiesDataReaderFactory entitiesDataReaderFactory;
        private readonly IStatementParserFactory statementParserFactory;

        public IBBrokerParserProvider(
            IEntitiesDataReaderFactory entitiesDataReaderFactory, 
            IStatementParserFactory statementParserFactory) 
        {
            this.entitiesDataReaderFactory = entitiesDataReaderFactory;
            this.statementParserFactory = statementParserFactory;
        }
        public IEntitiesDataReader EntitiesDataReader => entitiesDataReaderFactory.CreateNew(BrokerCode.IB.ToString());

        public IStatementParser StatementParser => statementParserFactory.CreateNew(BrokerCode.IB.ToString());
    }
}

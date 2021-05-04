using System.Collections.Generic;
using Txc.Model.Statement;
using Txc.Model.Trades;

namespace Txc.Services
{
    public interface ITradesDataReader
    {
        List<Trade> ReadTrades(StatementData statementData);
    }


    public interface IFinInstrumentsDataReader
    {
        List<Trade> ReadTrades(StatementData statementData);
    }
}

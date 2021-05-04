using System.Collections.Generic;
using Txc.Model.Dividends;
using Txc.Model.Fees;
using Txc.Model.FinInstruments;
using Txc.Model.Interests;
using Txc.Model.SecuritiesLent;
using Txc.Model.Statement;
using Txc.Model.Trades;

namespace Txc.Services
{
    public interface IEntitiesDataReader
    {
        IList<TResult> ReadEntities<T, TResult>(StatementData statementData) where T : new();
        IList<TResult> Read<TResult>(StatementData statementData);

        //IList<Trade> ReadTrades(StatementData statementData);
        //IList<FinInstrument> ReadFinInstruments(StatementData statementData);
        //IList<Dividend> ReadDividends(StatementData statementData);
        //IList<SecuritiesLentInterest> ReadSecuritiesLentInterests(StatementData statementData);
        //IList<Interest> ReadInterests(StatementData statementData);
        //IList<Fee> ReadFees(StatementData statementData);
        

    }
}

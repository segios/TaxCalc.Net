using System.IO;
using Txc.Model.Statement;

namespace Txc.Services
{
    public interface IStatementParser
    {
        StatementData ParseStatement(Stream stream);
        StatementData ParseStatement(string filePath);
        StatementData ParseStatement(byte[] data);
    }
}

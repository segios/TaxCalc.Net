using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Txc.Model.Deposits;
using Txc.Model.Dividends;
using Txc.Model.Fees;
using Txc.Model.FinInstruments;
using Txc.Model.Interests;
using Txc.Model.SecuritiesLent;
using Txc.Model.Statement;
using Txc.Model.Trades;
using Txc.Model.ForexTrades;
using Txc.Model.AccountInformations;
using Txc.Services.IB.AutoMapperConfig;
using Txc.Services.IB.Extensions;
using Txc.Services.IB.Model;

namespace Txc.Services.IB
{

    public class IBEntitiesDataReader : EntityDataReader, IEntitiesDataReader
    {
        private static readonly Dictionary<Type, Type> ResultToTypeMap = new Dictionary<Type, Type>()
        {
            { typeof(Trade), typeof(IBTrade) },
            { typeof(FinInstrument), typeof(IBFinInstrument) },
            { typeof(Dividend), typeof(DividendAccrural) },
            { typeof(SecuritiesLentInterest), typeof(IBSecuritiesLentInterest) },
            { typeof(Interest), typeof(IBInterest) },
            { typeof(Fee), typeof(IBFee) },
            { typeof(Deposit), typeof(IBDeposit) },
            { typeof(ForexTrade), typeof(IBForexTrade) },
            { typeof(AccountInformation), typeof(IBAccountInformation) },
        };

        private readonly IMapper mapper;
        public IBEntitiesDataReader(IClassMappingFactory classMappingFactory, IBAutomapperFactory automapperFactory) : base(classMappingFactory)
        {
            mapper = automapperFactory.GetMapper();
        }

        public IList<TResult> ReadEntities<T, TResult>(StatementData statementData) where T : new()
        {
            var entities = ReadSection<T>(statementData);
            return mapper.Map<IList<T>, IList<TResult>>(entities);
        }

        public IList<TResult> Read<TResult>(StatementData statementData)
        {
            var ibType = ResultToTypeMap[typeof(TResult)];

            return ReadEntities<TResult>(ibType, statementData);
        }

        private IList<TResult> ReadEntities<TResult>(Type ibType, StatementData statementData) 
        {
            var entities = ReadSection(ibType, statementData);
            return mapper.Map<IList<TResult>>(entities);
        }
    }
}

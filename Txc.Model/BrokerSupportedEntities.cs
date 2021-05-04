using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Txc.Model
{
    public static class BrokerSupportedEntities
    {
        public static Dictionary<BrokerCode, EntityCode> Entities = new Dictionary<BrokerCode, EntityCode>() 
        {
            {
                BrokerCode.IB,  EntityCode.Trades |
                                EntityCode.Dividends | 
                                EntityCode.FinInstruments |
                                EntityCode.Interests |
                                EntityCode.Fees |
                                EntityCode.Deposits |
                                EntityCode.Forex
            }
        };
    }

    [Flags]
    public enum EntityCode 
    {
        Trades = 0x01,
        Dividends = 0x02,
        SecuritiesLentInterests = 0x04,
        FinInstruments = 0x08,
        Interests = 0x10,
        Fees = 0x20,
        Deposits = 0x40,
        Forex = 0x80
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Txc.Model.FinInstruments;
using Txc.Model.Trades;
using Txc.Services.IB.Model;

namespace Txc.Services.IB.Extensions
{
    public static class IBFinInstrumentExtensions
    {
        public static FinInstrument ToFinInstrument(this IBFinInstrument ibEntity)
        {
            return new FinInstrument()
            {
                //                AssetCategoryType = ibTrade.AssetCategoryType,
                AssetCategory = ibEntity.AssetCategory,
                Symbol = ibEntity.Symbol,
                Description = ibEntity.Description,
                Conid = ibEntity.Conid,
                SecurityID = ibEntity.SecurityID,
                ListingExch = ibEntity.ListingExch,
                Multiplier = ibEntity.Multiplier,
                Type = ibEntity.Type,
                Code = ibEntity.Code,

            };
        }
    }
}

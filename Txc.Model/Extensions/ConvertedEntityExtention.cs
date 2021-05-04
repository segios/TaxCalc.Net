using System;
using System.Collections.Generic;
using System.Linq;

namespace Txc.Model.Extensions
{
    public static class ConvertedEntityExtention
    {
        
        public static decimal CalcTotal<T>(this IEnumerable<ConvertedEntity<T>> assets)
            where T : IRateConvertable
        {
            var sum = assets.Select(x => x.ConvertedBasis).Sum();
            sum = Math.Abs(Math.Round(sum, 2));
            return sum;
        }
    }
}

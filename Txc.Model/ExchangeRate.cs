using Newtonsoft.Json;
using System;
using Txc.Model.Helpers;

namespace Txc.Model
{

    public struct ExchangeRate
    {
        [JsonConverter(typeof(UADateJsonConverter))]
        public DateTime Date;
        public decimal Rate;
    }

}

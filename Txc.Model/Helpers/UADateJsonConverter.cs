using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Txc.Model.Helpers
{
    public class UADateJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // implement in case you're serializing it back
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var dataString = (string)reader.Value;
            DateTime date = DateTime.ParseExact(dataString, "dd.MM.yyyy", null);

            return date;
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }
}

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Dynamic;

namespace Txc.Services.Extensions
{
    public static class JsonExtension
    {
        public static dynamic JsonToDynamic(this string json)
        {
            var obj = JsonConvert.DeserializeObject<ExpandoObject>(json, new ExpandoObjectConverter());
            return obj;
        }

        public static T JsonToType<T>(this string json)
        {
            var obj = JsonConvert.DeserializeObject<T>(json, new ExpandoObjectConverter());
            return obj;
        }

        public static object JsonToType(this string json, Type type)
        {
            var obj = JsonConvert.DeserializeObject(json, type, new ExpandoObjectConverter());
            return obj;
        }

        public static string ObjectToJson(this object obj)
        {
            if (obj == null) {
                return null;
            }

            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            return JsonConvert.SerializeObject(obj, jsonSerializerSettings);
        }
    }
}

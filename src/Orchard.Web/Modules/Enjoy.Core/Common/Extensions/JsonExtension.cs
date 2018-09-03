
namespace Enjoy.Core
{
    using System;
    using System.Data;
    using Newtonsoft.Json;
    using System.Linq;
    public static class JsonExtension
    {
        public static string SerializeToJson(this object input)
        {
            return JsonConvert.SerializeObject(input);
        }
        public static T DeserializeToObject<T>(this string input)
        {
            return JsonConvert.DeserializeObject<T>(input);
        }
        public static DbType? PredictDbTypeBySearchColumeValue(this object value)
        {
            var values = value as string[];
            if (values == null)
            {
                values = new string[] { value.ToString() };
            };
            if (values.All(o => string.IsNullOrWhiteSpace(o))) return null;
            if (values.All((ctx) => { return Int32.TryParse(value.ToString(), out Int32 mint); }))
            { return DbType.Int32; }

            if (values.All((ctx) => { return Int64.TryParse(value.ToString(), out Int64 mlong); }))
            { return DbType.Int64; }

            if (values.All((ctx) => { return Decimal.TryParse(value.ToString(), out Decimal mlong); }))
            { return DbType.Int64; }

            if (values.All((ctx) => { return DateTime.TryParse(value.ToString(), out DateTime datetime); }))
            { return DbType.DateTime; }

            return DbType.String;
        }
    }
}

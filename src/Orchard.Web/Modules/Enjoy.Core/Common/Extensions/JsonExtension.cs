
namespace Enjoy.Core
{
    using System;
    using Newtonsoft.Json;
    public static class JsonExtension
    {        
        public static string ToJson(this object input)
        {
            return JsonConvert.SerializeObject(input);
        }      
        public static T DeserializeToObject<T>(this string input)
        {
            return JsonConvert.DeserializeObject<T>(input);
        }
    }
}

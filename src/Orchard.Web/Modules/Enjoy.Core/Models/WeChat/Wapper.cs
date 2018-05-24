

namespace Enjoy.Core.Models
{
    using Newtonsoft.Json;
    public class WapperWxResponse<T> : WxResponse
    {

        [JsonProperty("info")]
        public T Info { get; set; }
    }
    public class WapperWxRequest<T>
    {
        [JsonProperty("info")]
        public T Info { get; set; }
    }
}
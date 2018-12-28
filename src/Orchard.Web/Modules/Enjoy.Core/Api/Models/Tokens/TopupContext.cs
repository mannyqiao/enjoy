
namespace Enjoy.Core.ApiModels
{
    using Newtonsoft.Json;
    public class TopupContext
    {
        [JsonProperty("appid")]
        public string AppId { get; set; }

        [JsonProperty("money")]
        public int Money { get; set; }

        [JsonProperty("openid")]
        public string OpenId { get; set; }

        [JsonProperty("unionid")]
        public string UnionId { get; set; }

        [JsonProperty("cardid")]
        public string CardId { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
        
        
    }
}
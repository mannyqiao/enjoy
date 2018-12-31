

namespace Enjoy.Core.ApiModels
{
    using Newtonsoft.Json;
    public class SignatureContext
    {
        [JsonProperty("appId")]
        public string AppId { get; set; }
        [JsonProperty("secret")]
        public string AppSecret { get; set; }
        [JsonProperty("cardid")]
        public string CardId { get; set; }
        [JsonProperty("openid")]
        public string OpenId { get; set; }
    }
}
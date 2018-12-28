

namespace Enjoy.Core.ApiModels
{
    using Newtonsoft.Json;
    public class CardExtModel
    {
        //[JsonProperty("openid")]
        //public string OpenId { get; set; }

        [JsonProperty("timestamp")]
        public string TimeStamp { get; set; }

        [JsonProperty("signature")]
        public string Signature { get; set; }

        [JsonProperty("nonce_str")]
        public string NonceStr { get; set; }

        [JsonProperty("outer_str")]
        public string OuterStr { get; set; }
    }
}
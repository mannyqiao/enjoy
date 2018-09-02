
namespace Enjoy.Core.WeChatModels
{
    using Newtonsoft.Json;
    /// <summary>
    /// 来自于 H5授权所获取的微信用户信息
    /// </summary>
    public class WeChatUserH5Auth
    {
        [JsonProperty("openid")]
        public string OpenId { get; set; }
        [JsonProperty("nickname")]
        public string NickName { get; set; }
        [JsonProperty("sex")]
        public int Sex { get; set; }
        [JsonProperty("province")]
        public string Province { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("headimgurl")]
        public string HeadImageUrl { get; set; }
        [JsonProperty("privilege")]
        public string Privilege { get; set; }
        [JsonProperty("unionid")]
        public string UnionId { get; set; }
    }
}

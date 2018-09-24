
namespace Enjoy.Core.ApiModels
{
    using Newtonsoft.Json;
    public class BindMobileContext
    {
        [JsonProperty("eUserId")]
        public long EUserId { get; set; }

        [JsonProperty("unionid")]
        public string UnionId { get; set; }
        [JsonProperty("openid")]
        public string OpenId { get; set; }
        [JsonProperty("iv")]
        public string IV { get; set; }
        [JsonProperty("session_key")]
        public string SessionKey { get; set; }
        //[JsonProperty("id")]
        //public long Id { get; set; }
        //[JsonProperty("mobile")]
        //public string Mobile { get; set; }
        //[JsonProperty("verifyCode")]
        //public string VerifyCode { get; set; }
    }
}

namespace Enjoy.Core.ApiModels
{
    using Newtonsoft.Json;
    public class BindMobileContext
    {
       [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("mobile")]
        public string Mobile { get; set; }
        [JsonProperty("verifyCode")]
        public string VerifyCode { get; set; }
    }
}
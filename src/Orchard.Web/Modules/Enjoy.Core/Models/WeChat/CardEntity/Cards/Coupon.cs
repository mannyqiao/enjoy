namespace WeChat.Models
{
    using Newtonsoft.Json;
    using Enjoy.Core;
    public class Coupon
    {
        [JsonProperty("base_info")]
        public BaseInfo BaseInfo { get; set; }

        [JsonProperty("advanced_info")]
        public AdvancedInfo AdvancedInfo { get; set; }
    }
}
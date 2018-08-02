namespace Enjoy.Core.WeChatModels
{
    using Newtonsoft.Json;
    using Enjoy.Core;

    public abstract class CardCouponWapper
    {
        [JsonProperty("base_info")]
        public BaseInfo BaseInfo { get; set; }

        [JsonProperty("advanced_info")]
        public AdvancedInfo AdvancedInfo { get; set; }

    }
}
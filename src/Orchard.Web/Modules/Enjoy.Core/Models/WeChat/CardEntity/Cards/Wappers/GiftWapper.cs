namespace WeChat.Models
{
    using Newtonsoft.Json;
    using Enjoy.Core;
    public class GiftWapper : CardCouponWapper
    {
        #region gift 专用
        [JsonProperty("gift", NullValueHandling = NullValueHandling.Ignore)]
        public string Gift { get; set; }
        #endregion
    }
}
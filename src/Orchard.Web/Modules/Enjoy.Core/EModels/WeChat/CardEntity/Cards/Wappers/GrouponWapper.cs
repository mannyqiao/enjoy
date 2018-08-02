namespace WeChat.Models
{
    using Newtonsoft.Json;
    using Enjoy.Core;

    public class GrouponWapper : CardCouponWapper
    {
        #region groupon 专用

        [JsonProperty("deal_detail", NullValueHandling = NullValueHandling.Ignore)]
        public string DealDetail { get; set; }
        #endregion
    }
}
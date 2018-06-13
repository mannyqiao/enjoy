namespace WeChat.Models
{
    using Newtonsoft.Json;
    using Enjoy.Core;
    public class DiscountWapper : CardCouponWapper
    {
        #region discount 专用

        [JsonProperty("discount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Discount { get; set; }
        #endregion
    }
}
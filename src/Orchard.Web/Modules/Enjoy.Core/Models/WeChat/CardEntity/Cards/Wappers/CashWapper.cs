namespace WeChat.Models
{
    using Newtonsoft.Json;
    using Enjoy.Core;

    public class CashWapper : CardCouponWapper
    {
        #region cash 专用
        [JsonProperty("least_cost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? LeastCost { get; set; }

        [JsonProperty("reduce_cost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ReduceCost { get; set; }
        #endregion
    }
}
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

        #region discount 专用

        [JsonProperty("discount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Discount { get; set; }
        #endregion

        #region cash 专用
        [JsonProperty("least_cost",NullValueHandling = NullValueHandling.Ignore)]
        public decimal? LeastCost { get; set; }

        [JsonProperty("reduce_cost",NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ReduceCost { get; set; }
        #endregion

        #region groupon 专用

        [JsonProperty("deal_detail",NullValueHandling = NullValueHandling.Ignore)]
        public string DealDetail { get; set; }
        #endregion

        #region  general
        [JsonProperty("default_detail",NullValueHandling = NullValueHandling.Ignore)]
        public string DefaultDetail { get; set; }
        #endregion

        #region gift 专用
        [JsonProperty("gift",NullValueHandling = NullValueHandling.Ignore)]
        public string Gift { get; set; }
        #endregion
    }
}
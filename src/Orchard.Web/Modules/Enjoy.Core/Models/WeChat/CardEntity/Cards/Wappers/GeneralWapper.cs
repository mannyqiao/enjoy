namespace WeChat.Models
{
    using Newtonsoft.Json;
    using Enjoy.Core;
    public class GeneralWapper : CardCouponWapper
    {
        #region  general
        [JsonProperty("default_detail", NullValueHandling = NullValueHandling.Ignore)]
        public string DefaultDetail { get; set; }
        #endregion
    }
}
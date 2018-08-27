namespace Enjoy.Core.WeChatModels
{
    using Newtonsoft.Json;
    using Enjoy.Core;
    using System;

    /// <summary>
    /// 团购券
    /// </summary>
    [Serializable]
    public class Groupon : StandardCardCoupon
    {
        #region groupon 专用

        [JsonProperty("deal_detail", NullValueHandling = NullValueHandling.Ignore)]
        public string DealDetail { get; set; }
        #endregion

    }
}
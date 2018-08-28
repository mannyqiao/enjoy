namespace Enjoy.Core.WeChatModels
{
    using Newtonsoft.Json;
    using Enjoy.Core;
    using System;
    [Serializable]
    public class GiftCoupon: StandardCardCoupon
    {
        #region gift 专用
        [JsonProperty("gift", NullValueHandling = NullValueHandling.Ignore)]
        public string Gift { get; set; }
        #endregion

    }
}
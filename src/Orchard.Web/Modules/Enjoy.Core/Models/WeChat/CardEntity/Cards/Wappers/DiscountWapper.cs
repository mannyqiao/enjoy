namespace Enjoy.Core.WeChatModels
{
    using Newtonsoft.Json;
    using Enjoy.Core;
    public class DiscountWapper : CardCouponWapper
    {
        #region discount 专用
        private int? discount;
        [JsonProperty("discount", NullValueHandling = NullValueHandling.Ignore)]
        public int? Discount
        {
            get { return (discount ?? 0) / 100; }
            set { this.discount = value * 100; }
        }
        #endregion
    }
}
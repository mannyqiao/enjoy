namespace Enjoy.Core.WeChatModels
{
    using Newtonsoft.Json;
    using Enjoy.Core;
    public class DiscountWapper : CardCoupon<DiscountCoupon>
    {
        [JsonProperty("discount")]
        public override DiscountCoupon Card
        {
            get; set;
        }

    }
}
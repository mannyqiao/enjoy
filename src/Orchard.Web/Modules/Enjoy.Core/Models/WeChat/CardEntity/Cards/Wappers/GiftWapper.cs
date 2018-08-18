namespace Enjoy.Core.WeChatModels
{
    using Newtonsoft.Json;
    using Enjoy.Core;
    public class GiftWapper : CardCoupon<GiftCoupon>
    {
        [JsonProperty("gift")]
        public override GiftCoupon Card { get; set; }        
    }
}
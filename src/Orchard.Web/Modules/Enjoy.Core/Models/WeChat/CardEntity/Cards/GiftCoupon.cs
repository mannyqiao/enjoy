namespace Enjoy.Core.WeChatModels
{
    using Newtonsoft.Json;
    using Enjoy.Core;
    using System;

    public class GiftCoupon: BaseCardCoupon<GiftWapper>
    {

        [JsonProperty("card_type")]
        public override string CardType
        {
            get
            {
                return CardTypes.GIFT.ToString();
            }
        }

        [JsonProperty("groupon")]
        public override GiftWapper CardCoupon { get; set; }
        
    }
}
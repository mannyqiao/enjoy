namespace Enjoy.Core.WeChatModels
{
    using Newtonsoft.Json;
    using Enjoy.Core;
    using System;

    public class DiscountCoupon: BaseCardCoupon<DiscountWapper>
    {
        [JsonProperty("card_type")]
        public override string CardType
        {
            get
            {
                return CardTypes.DISCOUNT.ToString();
            }           
        }

        [JsonProperty("discount")]
        public override DiscountWapper CardCoupon { get; set; }

    }
}
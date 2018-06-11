namespace WeChat.Models
{
    using Newtonsoft.Json;
    using Enjoy.Core;
    using System;

    public class GiftCoupon: ICardCoupon
    {

        [JsonProperty("card_type")]
        public string CardType
        {
            get
            {
                return CardTypes.GIFT.ToString();
            }
            set { }
        }

        [JsonProperty("groupon")]
        public Coupon Coupon { get; set; }

        [JsonProperty("gift")]
        public string Gift { get; set; }
        public void Specific(Action<BaseInfo, AdvancedInfo> action)
        {
            action(Coupon.BaseInfo, Coupon.AdvancedInfo);
        }
    }
}
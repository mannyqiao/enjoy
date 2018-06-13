namespace WeChat.Models
{
    using Newtonsoft.Json;
    using Enjoy.Core;
    using System;

    public class DiscountCoupon: ICardCoupon
    {
        [JsonProperty("card_type")]
        public string CardType
        {
            get
            {
                return CardTypes.DISCOUNT.ToString();
            }           
        }


        [JsonProperty("discount")]
        public Coupon Coupon { get; set; }

      

        public void Specific(Action<BaseInfo, AdvancedInfo> action)
        {
            action(Coupon.BaseInfo, Coupon.AdvancedInfo);
        }
    }
}
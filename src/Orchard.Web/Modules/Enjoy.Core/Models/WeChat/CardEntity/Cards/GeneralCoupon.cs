namespace WeChat.Models
{
    using Newtonsoft.Json;
    using Enjoy.Core;
    using System;

    public class GeneralCoupon: ICardCoupon
    {
        [JsonProperty("card_type")]
        public string CardType
        {
            get
            {
                return CardTypes.GENERAL_COUPON.ToString();
            }
            
        }

        [JsonProperty("groupon")]
        public Coupon Coupon { get; set; }

        [JsonProperty("default_detail")]
        public string DefaultDetail { get; set; }

        public void Specific(Action<BaseInfo, AdvancedInfo> action)
        {
            action(Coupon.BaseInfo, Coupon.AdvancedInfo);
        }
        //default_detail":"优惠券专用，填写优惠详情"
    }
}
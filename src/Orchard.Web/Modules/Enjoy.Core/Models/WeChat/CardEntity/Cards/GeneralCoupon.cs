namespace WeChat.Models
{
    using Newtonsoft.Json;
    using Enjoy.Core;
    public class GeneralCoupon
    {
        [JsonProperty("card_type")]
        public string CardType
        {
            get
            {
                return CardTypes.GENERAL_COUPON.ToString();
            }
            set { }
        }

        [JsonProperty("groupon")]
        public Coupon Coupon { get; set; }

        [JsonProperty("default_detail")]
        public string DefaultDetail { get; set; }
        //default_detail":"优惠券专用，填写优惠详情"
    }
}
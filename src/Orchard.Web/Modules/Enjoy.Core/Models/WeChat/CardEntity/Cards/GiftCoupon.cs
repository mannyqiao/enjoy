namespace WeChat.Models
{
    using Newtonsoft.Json;
    using Enjoy.Core;
    public class GiftCoupon
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
    }
}
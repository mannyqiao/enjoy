namespace WeChat.Models
{
    using Newtonsoft.Json;
    using Enjoy.Core;
    public class DiscountCoupon
    {
        [JsonProperty("card_type")]
        public string CardType
        {
            get
            {
                return CardTypes.DISCOUNT.ToString();
            }
            set { }
        }


        [JsonProperty("discount")]
        public Coupon Coupon { get; set; }

        [JsonProperty("discount")]
        public int Discount { get; set; }
    }
}
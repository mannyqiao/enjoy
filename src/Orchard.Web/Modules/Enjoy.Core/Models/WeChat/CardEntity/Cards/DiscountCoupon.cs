namespace WeChat.Models
{
    using Newtonsoft.Json;
    using Enjoy.Core;
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


        [JsonProperty("card")]
        public Coupon Coupon { get; set; }

        [JsonProperty("discount")]
        public decimal Discount { get; set; }
    }
}
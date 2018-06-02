namespace WeChat.Models
{
    using Newtonsoft.Json;
    using Enjoy.Core;
    /// <summary>
    /// 团购券
    /// </summary>
    public class Groupon
    {
        [JsonProperty("card_type")]
        public string CardType
        {
            get
            {
                return CardTypes.GROUPON.ToString();
            }
            set { }
        }

        [JsonProperty("groupon")]
        public Coupon Coupon { get; set; }

        [JsonProperty("deal_detail")]
        public string DealDetail { get; set; }
    }
}
namespace WeChat.Models
{
    using Newtonsoft.Json;
    using Enjoy.Core;
    /// <summary>
    /// 现金券
    /// </summary>
    public class Cash
    {
        [JsonProperty("card_type")]
        public string CardType
        {
            get
            {
                return CardTypes.CASH.ToString();
            }
            set { }
        }

        [JsonProperty("cash")]
        public Coupon Coupon { get; set; }

        [JsonProperty("least_cost")]
        public int LeastCost { get; set; }

        [JsonProperty("reduce_cost")]
        public int ReduceCost { get; set; }
    }
}
namespace WeChat.Models
{
    using Newtonsoft.Json;
    using Enjoy.Core;
    using System;

    /// <summary>
    /// 现金券
    /// </summary>
    public class CashCoupon : ICardCoupon
    {
        [JsonProperty("card_type")]
        public string CardType
        {
            get
            {
                return CardTypes.CASH.ToString();
            }
        }

        [JsonProperty("cash")]
        public Coupon Coupon { get; set; }

        [JsonProperty("least_cost")]
        public decimal LeastCost { get; set; }

        [JsonProperty("reduce_cost")]
        public decimal ReduceCost { get; set; }

        public void Specific(Action<BaseInfo, AdvancedInfo> action)
        {
            action(Coupon.BaseInfo, Coupon.AdvancedInfo); 
        }
    }
}
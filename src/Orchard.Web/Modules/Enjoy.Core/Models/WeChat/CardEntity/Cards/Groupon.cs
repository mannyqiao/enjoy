namespace WeChat.Models
{
    using Newtonsoft.Json;
    using Enjoy.Core;
    using System;

    /// <summary>
    /// 团购券
    /// </summary>
    public class Groupon: ICardCoupon
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

 



        public void Specific(Action<BaseInfo, AdvancedInfo> action)
        {
            action(Coupon.BaseInfo, Coupon.AdvancedInfo);
        }
    }
}
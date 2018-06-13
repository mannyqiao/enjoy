namespace WeChat.Models
{
    using Newtonsoft.Json;
    using Enjoy.Core;
    using System;
    public abstract class BaseCardCoupon<TWapper> : ICardCoupon
        where TWapper : CardCouponWapper
    {
        [JsonProperty("card_type")]
        public virtual string CardType
        {
            get
            {
                throw new NotImplementedException("must override by child class");
            }
        }


        [JsonProperty("cash")]
        public abstract TWapper CardCoupon { get; set; }



        public void Specific(Action<BaseInfo, AdvancedInfo> action)
        {
            action(CardCoupon.BaseInfo, CardCoupon.AdvancedInfo);
        }
    }
    //public abstract class CardCouponWapper
    //{
    //    [JsonProperty("base_info")]
    //    public BaseInfo BaseInfo { get; set; }

    //    [JsonProperty("advanced_info")]
    //    public AdvancedInfo AdvancedInfo { get; set; }
    //}
}
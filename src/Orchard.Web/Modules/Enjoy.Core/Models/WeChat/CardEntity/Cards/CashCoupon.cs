namespace WeChat.Models
{
    using Newtonsoft.Json;
    using Enjoy.Core;
    using System;

    /// <summary>
    /// 现金券
    /// </summary>
    public class CashCoupon : BaseCardCoupon<CashWapper>
    {
        [JsonProperty("card_type")]
        public override string CardType
        {
            get
            {
                return CardTypes.CASH.ToString();
            }
        }


        [JsonProperty("cash")]
        
        public override CashWapper CardCoupon { get; set; }
    }
}
namespace Enjoy.Core.WeChatModels
{
    using Newtonsoft.Json;
    using Enjoy.Core;
    using System;

    /// <summary>
    /// 团购券
    /// </summary>
    public class Groupon : BaseCardCoupon<GrouponWapper>
    {
        [JsonProperty("card_type")]
        public override string CardType
        {
            get
            {
                return CardTypes.GROUPON.ToString();
            }

        }

        [JsonProperty("groupon")]
        public override GrouponWapper CardCoupon { get; set; }
       
    }
}
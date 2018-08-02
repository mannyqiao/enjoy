namespace Enjoy.Core.WeChatModels
{
    using Newtonsoft.Json;
    using Enjoy.Core;
    using System;

    public class GeneralCoupon: BaseCardCoupon<GeneralWapper>
    {
        [JsonProperty("card_type")]
        public override string CardType
        {
            get
            {
                return CardTypes.GENERAL_COUPON.ToString();
            }
            
        }

        [JsonProperty("groupon")]
        public override GeneralWapper CardCoupon { get; set; }

      

        
        //default_detail":"优惠券专用，填写优惠详情"
    }
}
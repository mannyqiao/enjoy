namespace Enjoy.Core.WeChatModels
{
    using Newtonsoft.Json;
    using Enjoy.Core;
    using System;

    public class GeneralCoupon: StandardCardCoupon
    {
       
        [JsonProperty("default_detail")]
        public string DefaultDetail { get; set; }

        //default_detail":"优惠券专用，填写优惠详情"
    }
}
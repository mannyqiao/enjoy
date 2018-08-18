namespace Enjoy.Core.WeChatModels
{
    using Newtonsoft.Json;
    using Enjoy.Core;
    public class GeneralWapper : CardCoupon<GeneralCoupon>
    {
        [JsonProperty("general")]
        public override GeneralCoupon Card { get; set; }


    }
}
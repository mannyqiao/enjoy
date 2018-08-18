namespace Enjoy.Core.WeChatModels
{
    using Newtonsoft.Json;
    using Enjoy.Core;

    public class CashWapper : CardCoupon<CashCoupon>
    {

        [JsonProperty("cash")]
        public override CashCoupon Card {
            get;set;
        }
    }
}
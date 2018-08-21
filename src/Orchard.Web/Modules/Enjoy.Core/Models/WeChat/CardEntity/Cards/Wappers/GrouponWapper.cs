namespace Enjoy.Core.WeChatModels
{
    using Newtonsoft.Json;
    using Enjoy.Core;

    public class GrouponWapper :  CardCoupon<Groupon>
    {
        [JsonProperty("groupon")]
        public override Groupon Card { get; set; }
        
    }
}
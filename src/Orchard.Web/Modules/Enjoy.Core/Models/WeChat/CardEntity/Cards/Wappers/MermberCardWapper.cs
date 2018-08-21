namespace Enjoy.Core.WeChatModels
{
    using Newtonsoft.Json;
    using Enjoy.Core;
    public class MemberCardWapper : CardCoupon<MemberCard>
    {
        [JsonProperty("member_card")]
        public override MemberCard Card
        {
            get; set;
        }

    }
}
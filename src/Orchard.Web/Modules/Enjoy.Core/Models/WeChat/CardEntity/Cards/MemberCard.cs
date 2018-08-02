namespace Enjoy.Core.WeChatModels
{
    using Newtonsoft.Json;
    using Enjoy.Core;
    using System;
    public class MemberCard : BaseCardCoupon<MerberCardWapper>
    {
        [JsonProperty("card_type")]
        public override string CardType
        {
            get
            {
                return CardTypes.MEMBER_CARD.ToString();
            }
        }

        [JsonProperty("member_card")]
        public override MerberCardWapper CardCoupon { get; set; }


}
}
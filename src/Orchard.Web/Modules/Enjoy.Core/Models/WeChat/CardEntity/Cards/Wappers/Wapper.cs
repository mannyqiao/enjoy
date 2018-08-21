

namespace Enjoy.Core.WeChatModels
{
    using Enjoy.Core.WeChatModels;
    using Newtonsoft.Json;
    public class CreatingWapper
    {
        [JsonProperty("card", NullValueHandling = NullValueHandling.Ignore)]
        public CashWapper Cash { get; set; }

        [JsonProperty("card", NullValueHandling = NullValueHandling.Ignore)]
        public DiscountWapper Discount { get; set; }

        [JsonProperty("card", NullValueHandling = NullValueHandling.Ignore)]
        public GrouponWapper Groupon { get; set; }

        [JsonProperty("card", NullValueHandling = NullValueHandling.Ignore)]
        public GeneralWapper General { get; set; }

        [JsonProperty("card", NullValueHandling = NullValueHandling.Ignore)]
        public GiftWapper Gift { get; set; }

        [JsonProperty("card", NullValueHandling = NullValueHandling.Ignore)]
        public MemberCardWapper MemberCard { get; set; }

    }
    public class UpgradeWapper
    {
        [JsonProperty("card_id")]
        public string CardId { get; set; }

        [JsonProperty("cash", NullValueHandling = NullValueHandling.Ignore)]
        public CashCoupon Cash { get; set; }

        [JsonProperty("discount", NullValueHandling = NullValueHandling.Ignore)]
        public DiscountCoupon Discount { get; set; }

        [JsonProperty("groupon", NullValueHandling = NullValueHandling.Ignore)]
        public Groupon Groupon { get; set; }

        [JsonProperty("general_coupon", NullValueHandling = NullValueHandling.Ignore)]
        public GeneralCoupon General { get; set; }

        [JsonProperty("gift", NullValueHandling = NullValueHandling.Ignore)]
        public GiftCoupon Gift { get; set; }

        [JsonProperty("member_card", NullValueHandling = NullValueHandling.Ignore)]
        public MemberCard MemberCard { get; set; }
    }
}
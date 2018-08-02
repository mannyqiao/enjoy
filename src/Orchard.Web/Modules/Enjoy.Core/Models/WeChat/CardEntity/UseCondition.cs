namespace Enjoy.Core.WeChatModels
{
    using Newtonsoft.Json;
    using System.ComponentModel;

    public class UseCondition
    {
        [JsonProperty("accept_category", NullValueHandling = NullValueHandling.Ignore)]
        public string AcceptCategory { get; set; }

        [JsonProperty("reject_category",NullValueHandling= NullValueHandling.Ignore)]
        public string RejectCategory { get; set; }

        [JsonProperty("can_use_with_other_discount")]
        [DefaultValue(true)]
        public bool CanUseWithOtherDiscount { get; set; }
    }
}
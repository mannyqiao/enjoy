
namespace WeChat.Models
{
    using Newtonsoft.Json;
    public class UseCondition
    {
        [JsonProperty("accept_category")]
        public string AcceptCategory { get; set; }

        [JsonProperty("reject_category")]
        public string RejectCategory { get; set; }

        [JsonProperty("can_use_with_other_discount")]
        public bool CanUseWithOtherDiscount { get; set; }
    }
}
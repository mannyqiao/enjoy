namespace Enjoy.Core.WeChatModels
{
    using Newtonsoft.Json;
    using System.ComponentModel;

    public class UseCondition
    {
        [JsonProperty("accept_category", NullValueHandling = NullValueHandling.Ignore)]
        public string AcceptCategory { get; set; }

        [JsonProperty("reject_category", NullValueHandling = NullValueHandling.Ignore)]
        public string RejectCategory { get; set; }
        /// <summary>
        /// 满减门槛字段，可用于兑换券和代金券 ，填入后将在全面拼写消费满xx元可用。
        /// </summary>
        private int? least_cost = null;
        [Newtonsoft.Json.JsonProperty("least_cost", NullValueHandling = NullValueHandling.Ignore)]
        public int? LeastCost
        {
            get
            {
                return (least_cost ?? 0) / 100;
            }
            set
            {
                this.least_cost = (value == null || value == 0) ? null : value * 100;
            }
        }

        [JsonProperty("can_use_with_other_discount")]
        [DefaultValue(true)]
        public bool CanUseWithOtherDiscount { get; set; }

        [JsonIgnore]
        public UseLimitTypes Type
        {
            get
            {
                return (this.LeastCost == null || this.LeastCost.Value.Equals(0))
                    ? UseLimitTypes.None
                    : UseLimitTypes.Specified;
            }
        }
    }
}
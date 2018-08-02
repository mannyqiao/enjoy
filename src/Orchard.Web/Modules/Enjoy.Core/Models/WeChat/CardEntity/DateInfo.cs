

namespace Enjoy.Core.WeChatModels
{
    using Newtonsoft.Json;
    public class DateInfo
    {
        [JsonProperty("type")]
        public string Type { get; set; }


        [JsonProperty("begin_timestamp", NullValueHandling = NullValueHandling.Ignore)]
        public long? BeginTimestamp { get; set; }


        [JsonProperty("end_timestamp",NullValueHandling= NullValueHandling.Ignore)]
        public long? EndTimestamp { get; set; }

        [JsonProperty("fixed_term", NullValueHandling = NullValueHandling.Ignore)]
        public int? FixedTerm { get; set; }

        [JsonProperty("fixed_begin_term", NullValueHandling = NullValueHandling.Ignore)]
        public int? FixedBeginTerm { get; set; }
    }
}
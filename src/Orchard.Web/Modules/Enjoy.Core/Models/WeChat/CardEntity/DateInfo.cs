

namespace WeChat.Models
{
    using Newtonsoft.Json;
    public class DateInfo
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("begin_timestamp")]
        public long BeginTimestamp { get; set; }

        [JsonProperty("end_timestamp")]
        public long EndTimestamp { get; set; }
    }
}
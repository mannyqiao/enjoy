namespace Enjoy.Core.WeChatModels
{
    using Newtonsoft.Json;
    public class TimeLimit
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("begin_hour", NullValueHandling = NullValueHandling.Ignore)]
        public int? BeginHour { get; set; }

        [JsonProperty("end_hour", NullValueHandling = NullValueHandling.Ignore)]
        public int? EndHour { get; set; }

        [JsonProperty("begin_minute", NullValueHandling = NullValueHandling.Ignore)]
        public int? BeginMinute { get; set; }

        [JsonProperty("end_minute", NullValueHandling = NullValueHandling.Ignore)]
        public int? EndMinute { get; set; }
    }
}
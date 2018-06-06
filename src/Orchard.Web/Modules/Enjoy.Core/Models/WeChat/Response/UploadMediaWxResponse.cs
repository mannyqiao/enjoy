

namespace Enjoy.Core.Models
{
    public class UploadMediaWxResponse : WxResponse
    {
        [Newtonsoft.Json.JsonProperty("media_id")]
        public string MediaId { get; set; }

        [Newtonsoft.Json.JsonProperty("created_at")]
        public long CreatedAt { get; set; }

        [Newtonsoft.Json.JsonProperty("value")]
        public string Value { get; set; }

        [Newtonsoft.Json.JsonProperty("url",NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Url { get; set; }
    }
}
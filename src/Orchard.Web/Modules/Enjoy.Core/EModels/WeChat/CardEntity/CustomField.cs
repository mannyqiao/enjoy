namespace WeChat.Models
{
    using Enjoy.Core;
    using Newtonsoft.Json;
    public class CustomField
    {
        [JsonProperty("name_type")]
        public string NameType { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
        //"name_type": "FIELD_NAME_TYPE_LEVEL",
          //      "url": "http://www.qq.com"
    }
}
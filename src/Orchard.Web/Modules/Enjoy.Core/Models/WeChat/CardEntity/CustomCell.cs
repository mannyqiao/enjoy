namespace WeChat.Models
{
    using Enjoy.Core;
    using Newtonsoft.Json;
    public class CustomCell
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("tips")]
        public string Tips { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        //  "custom_cell1": {
        //      "name": "使用入口2",
        //      "tips": "激活后显示",
        //      "url": "http://www.xxx.com"
        //  },
    }
}
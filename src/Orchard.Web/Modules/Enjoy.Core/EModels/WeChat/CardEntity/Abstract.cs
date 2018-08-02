

namespace WeChat.Models
{
    using Newtonsoft.Json;
    public class Abstract
    {
        [Newtonsoft.Json.JsonProperty("abstract")]
        public virtual string AbstractX { get; set; }

        [Newtonsoft.Json.JsonProperty("icon_url_list")]
        public virtual string[] IconUrlList { get; set; }
    }
}
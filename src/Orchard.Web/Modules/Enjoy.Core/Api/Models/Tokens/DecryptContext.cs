

namespace Enjoy.Core.ApiModels
{
    using Enjoy.Core.WeChatModels;
    using Newtonsoft.Json;
    public class DecryptContext
    {
        [JsonProperty("appid")]
        public string AppId { get; set; }
        [JsonProperty("data")]
        public string Data { get; set; }
        [JsonProperty("iv")]
        public string IV { get; set; }

        [JsonProperty("sessionKey")]
        public string SessionKey { get; set; }

        [JsonProperty("wx")]
        public WeChatUserInfo WxChatUser { get; set; }

    }
}
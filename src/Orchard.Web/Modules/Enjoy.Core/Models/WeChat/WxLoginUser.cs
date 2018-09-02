

namespace Enjoy.Core.WeChatModels
{
    using Newtonsoft.Json;
    public class WxLoginUser : IWxLoginUser
    {
        [JsonProperty("subscribe")]
        public int Subscribe { get; set; }
        [JsonProperty("openid")]
        public string Openid { get; set; }
        [JsonProperty("nickname")]
        public string Nickname { get; set; }
        [JsonProperty("sex")]
        public int Sex { get; set; }
        [JsonProperty("language")]
        public string Language { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("province")]
        public string Province { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("headimgurl")]
        public string Headimgurl { get; set; }
        [JsonProperty("subscribe_time")]
        public long Subscribe_time { get; set; }
        [JsonProperty("unionid")]
        public string Unionid { get; set; }
        [JsonProperty("remark")]
        public string Remark { get; set; }
        [JsonProperty("groupid")]
        public int Groupid { get; set; }
        [JsonProperty("tagid_list")]
        public int[] Tagid_list { get; set; }
        [JsonProperty("subscribe_scene")]
        public string Subscribe_scene { get; set; }

        [JsonProperty("qr_scene")]
        public int Qr_Scene { get; set; }

        [JsonProperty("qr_scene_str")]
        public string Qr_Scene_Str { get; set; }
    }
}
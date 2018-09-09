
namespace Enjoy.Core.WeChatModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System;
    using Enjoy.Core.ApiModels;

    public class WeChatUserInfo
    {
        [Newtonsoft.Json.JsonProperty("id")]
        public long Id { get; set; }

        [Newtonsoft.Json.JsonProperty("openid")]
        public string OpenId { get; set; }
        [Newtonsoft.Json.JsonProperty("nickName")]
        public string NickName { get; set; }
        [Newtonsoft.Json.JsonProperty("gender")]
        public string Gender { get; set; }
        [Newtonsoft.Json.JsonProperty("city")]
        public string City { get; set; }
        [Newtonsoft.Json.JsonProperty("province")]
        public string Province { get; set; }
        [Newtonsoft.Json.JsonProperty("country")]
        public string Country { get; set; }

        [Newtonsoft.Json.JsonProperty("avatarUrl")]
        public string AvatarUrl { get; set; }

        [Newtonsoft.Json.JsonProperty("unionId")]
        public string UnionId { get; set; }

        [Newtonsoft.Json.JsonProperty("state")]
        public UserState State { get; set; }

        [Newtonsoft.Json.JsonProperty("watermark")]
        public Watermark watermark { get; set; }

        
        
        public class Watermark
        {
            [Newtonsoft.Json.JsonProperty("appid")]
            public string AppId { get; set; }

            [Newtonsoft.Json.JsonProperty("timestamp")]            
            public string TimeStamp { get; set; }
        }
    }
}

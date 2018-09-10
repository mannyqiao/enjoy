using Enjoy.Core.EnjoyModels;
using Enjoy.Core.WeChatModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.ApiModels
{
    /// <summary>
    /// 登记微信用户
    /// </summary>
    public class RegWxUserContext
    {
        [JsonProperty("wx")]
        public WeChatUserInfo WxUser { get; set; }

        [JsonProperty("openid")]
        public string OpenId { get; set; }

        [JsonProperty("unionId")]
        public string UnionId { get; set; }
    }
}

namespace Enjoy.Core.WeChatModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System;
    public class WxAccessToken : IWxAccessToken
    {
        public WxAccessToken() { }
        public WxAccessToken(string token, string state, int expriesin)
        {
            this.Token = token;
            this.Expiresin = expriesin;
        }
        [Newtonsoft.Json.JsonProperty("access_token")]
        public string Token { get; set; }
        [Newtonsoft.Json.JsonProperty("expires_in")]
        public int Expiresin { get; set; }

        [Newtonsoft.Json.JsonProperty("openid")]
        public string OpenId { get; set; }

        [Newtonsoft.Json.JsonProperty("refesh_token")]
        public string RefreshToken { get; set; }

        [Newtonsoft.Json.JsonProperty("scope")]
        public string Scope { get; set; }

        [Newtonsoft.Json.JsonProperty("login_user")]
        public IWxLoginUser LoginUser { get; set; }
    }
}

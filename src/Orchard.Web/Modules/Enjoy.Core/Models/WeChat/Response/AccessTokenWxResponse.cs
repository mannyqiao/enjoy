using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.WeChatModels
{
    public class AccessTokenWxResponse : WxResponse
    {
        public AccessTokenWxResponse(string token, int expriesin)
        {
            this.Token = token;
            this.Expiresin = expriesin;
        }
        public AccessTokenWxResponse() { }
        [Newtonsoft.Json.JsonProperty("access_token")]
        public string Token { get; set; }
        [Newtonsoft.Json.JsonProperty("expires_in")]
        public int Expiresin { get; set; }
    }
}
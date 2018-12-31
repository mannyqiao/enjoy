

namespace Enjoy.Core.ApiModels
{
    using Newtonsoft.Json;
    public class JSCodeContext
    {
        [JsonProperty("appId")]
        public string AppId { get; set; }
        [JsonProperty("secret")]
        public string Secret { get; set; }
        [JsonProperty("js_code")]
        public string Code { get; set; }

        [JsonProperty("grant_type")]
        public string GrantType { get; set; }
        //            {
        //	 "appid": "wx3ec55fbaa7dcefc7",
        //     "secret": "e1374e932b2eef3d4b0fa8f0e936496a",
        //     "code": "011AAiSm0aqr6s1fmoTm036bSm0AAiS2",
        //     "grant_type": "authorization_code"
        //}
    }
}
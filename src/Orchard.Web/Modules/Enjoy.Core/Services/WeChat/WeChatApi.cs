
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Services
{
    using Orchard.Caching;
    using Orchard.Services;
    using Enjoy.Core.Models;
    using System;
    public class WeChatApi : IWeChatApi
    {
        private readonly ICacheManager Cache;
        private readonly IClock Clock;
        public const string CacheKey_Token = "Enjoy_WeChat_Token";
        public const string CacheKey_ApplyProtocol = "Enjoy_WeChat_ApplyProtocol";

        public WeChatApi(ICacheManager cache, IClock clock)
        {
            this.Cache = cache;
            this.Clock = clock;
        }
        public string GetToken(string appid, string appsecret)
        {
            return this.Cache.Get(CacheKey_Token, ctx =>
            {
                var token = WeChatApiRequestBuilder.GenerateWxTokenRequestUrl(appid, appsecret).GetResponseForJson<AccessTokenWxResponse>();
                ctx.Monitor(this.Clock.When(TimeSpan.FromSeconds(token.Expiresin)));//默认过期时间为 7200秒
                return token.Token;
            });
        }
        public string GetToken()
        {
            return GetToken("wx0c644f8027d78c74", "f1681068dfcd75ef2d7dff14cb3b5fae");
        }
        public ApplyProtocolWxResponse GetApplyProtocol()
        {
            return this.Cache.Get(CacheKey_ApplyProtocol, ctx =>
            {
                var apply = WeChatApiRequestBuilder.GenerateWxGetApplyProtocolUrl(GetToken())
                .GetResponseForJson<ApplyProtocolWxResponse>();
                ctx.Monitor(this.Clock.When(TimeSpan.FromDays(15)));//默认过期时间为 7200秒
                return apply;
            });
        }
    }
}
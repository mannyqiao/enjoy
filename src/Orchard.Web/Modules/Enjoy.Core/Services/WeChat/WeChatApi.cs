

namespace Enjoy.Core.Services
{
    using Orchard.Caching;
    using Orchard.Services;
    using Enjoy.Core.Models;
    using System;
    using System.Text;
    using System.IO;
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

        public UploadMediaWxResponse UploadMaterial(string name, byte[] buffers)
        {
            var request = WeChatApiRequestBuilder.GenerateWxUploaMediaUrl(this.GetToken(), MediaUploadTypes.AuthMaterial);
            return UploadMaterial(request, name, buffers);
        }

        public UploadMediaWxResponse UploadMaterialToCDN(byte[] buffers)
        {
            var request = WeChatApiRequestBuilder.GenerateWxUploaMediaUrl(this.GetToken(), MediaUploadTypes.Material);
            return UploadMaterial(request, "logo", buffers);
        }
        private UploadMediaWxResponse UploadMaterial(string url, string name, byte[] buffers)
        {
            return url.GetResponseForJson<UploadMediaWxResponse>((http) =>
            {
                http.Method = "POST";
                http.ContentType = "application/x-www-form-urlencoded";
                System.Net.CookieContainer cookieContainer = new System.Net.CookieContainer();
                http.CookieContainer = cookieContainer;
                http.AllowAutoRedirect = true;
                http.Method = "POST";
                string boundary = System.DateTime.Now.Ticks.ToString("X"); // 随机分隔线
                http.ContentType = "multipart/form-data;charset=utf-8;boundary=" + boundary;
                byte[] itemBoundaryBytes = System.Text.Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
                byte[] endBoundaryBytes = System.Text.Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");

                //// TODO :need change default filename
                var sbHeader = new System.Text.StringBuilder(string.Format(
                         "Content-Disposition:form-data;name=\"media\";filename=\"{0}\"\r\nContent-Type:application/octet-stream\r\n\r\n",
                         @"Enjoy.jpg"));
                byte[] postHeaderBytes = Encoding.UTF8.GetBytes(sbHeader.ToString());

                using (Stream postStream = http.GetRequestStream())
                {
                    postStream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
                    postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
                    postStream.Write(buffers, 0, buffers.Length);
                    postStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
                    postStream.Flush();
                }
                return http;
            });
        }
        public WxResponseWapper<CreateSubmerchantResponse> CreateSubmerchant(WxRequestWapper<SubMerchant> submerchant)
        {
            var request = WeChatApiRequestBuilder.GenerateWxCreateSubmerchantUrl(this.GetToken());
            return request.GetResponseForJson<WxResponseWapper<CreateSubmerchantResponse>>((http) =>
            {
                http.Method = "POST";
                http.ContentType = "application/json; encoding=utf-8";
                using (var stream = http.GetRequestStream())
                {
                    var body = submerchant.ToJson();
                    var buffers = UTF8Encoding.UTF8.GetBytes(body);
                    stream.Write(buffers, 0, buffers.Length);
                    stream.Flush();
                }
                return http;
            });
        }
    }
}


namespace Enjoy.Core.Services
{
    using Orchard.Caching;
    using Orchard.Services;
    using Enjoy.Core.Models;
    using System;
    using System.Text;
    using System.IO;
    using System.Security.Cryptography;
    using Orchard;
    using Orchard.Logging;

    public class WeChatApi : IWeChatApi
    {
        private readonly ICacheManager Cache;
        private readonly IClock Clock;
        private readonly IOrchardServices OS;
        public const string CacheKey_Token = "Enjoy_WeChat_Token";
        public const string CacheKey_ApplyProtocol = "Enjoy_WeChat_ApplyProtocol";
        public ILogger Logger;
        public WeChatApi(ICacheManager cache, IClock clock, IOrchardServices os)
        {
            this.Cache = cache;
            this.Clock = clock;
            this.OS = os;
            this.Logger = NullLogger.Instance;
        }
        public string GetToken(string appid, string appsecret)
        {
            return this.Cache.Get(CacheKey_Token, ctx =>
            {
                var token = WeChatApiRequestBuilder.GenerateWxTokenRequestUrl(appid, appsecret).GetResponseForJson<AccessTokenWxResponse>();
                ctx.Monitor(this.Clock.When(TimeSpan.FromSeconds(token.Expiresin)));//默认过期时间为 7200秒
                if (token.HasError) { Logger.Error(token.ErrMsg); }
                return token.Token;
            });
        }
        public string GetToken()
        {
            return GetToken(EnjoyConstant.Miniprogram.AppId, EnjoyConstant.Miniprogram.AppSecrect);
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
            return UploadMaterial(WeChatApiRequestBuilder.GenerateWxUploaMediaUrl(this.GetToken(), MediaUploadTypes.Material)
                 , "logo", buffers);
        }
        private UploadMediaWxResponse UploadMaterial(string url, string name, byte[] buffers)
        {
            var response = url.GetResponseForJson<UploadMediaWxResponse>((http) =>
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
            response.Value = response.MediaId ?? response.Url;
            if (string.IsNullOrEmpty(response.MediaId) == false)//if has mediaid
            {
                this.OS.CreateMediaDirectoryIfNotExits(EnjoyConstant.Directory_Media_Protocol_ROOT);
                var mediaFileName = this.OS.WorkContext.HttpContext.Server.MapPath(string.Concat(EnjoyConstant.Directory_Media_Protocol_ROOT, "/", response.MediaId, ".jpg"));
                if (File.Exists(mediaFileName)) File.Delete(mediaFileName);

                using (var stream = new FileStream(mediaFileName, FileMode.Create))
                {
                    stream.Write(buffers, 0, buffers.Length);
                    stream.Flush();
                }
            }
            response.Url = string.IsNullOrEmpty(response.MediaId) ? response.Url : WeChatApiRequestBuilder.GenrateImageUrlByMediaId(response.MediaId);
            return response;
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

        public WxSession CreateWxSession(IWxLoginUser loginUseer)
        {
            var request = WeChatApiRequestBuilder.GenerateWxAuthRequestUrl(EnjoyConstant.Miniprogram.AppId, loginUseer.Code, EnjoyConstant.Miniprogram.AppSecrect);
            var auth = request.GetResponseForJson<WeChatAuthorization>();
            var wechatUser = Decrypt(loginUseer.Data, loginUseer.IV, auth.SessionKey);
            return new WxSession() { LoginUser = loginUseer, Miniprogram = EnjoyConstant.Miniprogram, WeCharUser = wechatUser, Authorization = auth };
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="rawData">公开的用户资料</param>
        /// <param name="signature"></param>
        /// <param name="sessionKey"></param>
        /// <returns></returns>
        private bool VaildateSignature(string rawData, string signature, string sessionKey)
        {
            //创建SHA1签名类  
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            //编码用于SHA1验证的源数据  
            byte[] source = Encoding.UTF8.GetBytes(rawData + sessionKey);
            //生成签名  
            byte[] target = sha1.ComputeHash(source);
            //转化为string类型，注意此处转化后是中间带短横杠的大写字母，需要剔除横杠转小写字母  
            string result = BitConverter.ToString(target).Replace("-", "").ToLower();
            //比对，输出验证结果  
            return signature == result;
        }
        public string GetOpenId(IWxLoginUser loginUser)
        {
            return this.CreateWxSession(loginUser).WeCharUser.OpenId;
        }
        private WxUser Decrypt(string encryptedData, string iv, string sessionKey)
        {
#pragma warning disable IDE0017 // Simplify object initialization
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
#pragma warning restore IDE0017 // Simplify object initialization
            //设置解密器参数  
            aes.Mode = CipherMode.CBC;
            aes.BlockSize = 128;
            aes.Padding = PaddingMode.PKCS7;
            //格式化待处理字符串  
            byte[] byte_encryptedData = Convert.FromBase64String(encryptedData);
            byte[] byte_iv = Convert.FromBase64String(iv);
            byte[] byte_sessionKey = Convert.FromBase64String(sessionKey);

            aes.IV = byte_iv;
            aes.Key = byte_sessionKey;
            //根据设置好的数据生成解密器实例  
            ICryptoTransform transform = aes.CreateDecryptor();

            //解密  
            byte[] final = transform.TransformFinalBlock(byte_encryptedData, 0, byte_encryptedData.Length);

            //生成结果  
            string result = Encoding.UTF8.GetString(final);

            //反序列化结果，生成用户信息实例  
            return result.DeserializeToObject<WxUser>();
        }

        public IWxAuthorization GetWxAuth(IWxLoginUser loginUser)
        {
            throw new NotImplementedException();
        }


        public IWxAccessToken GetWxAccessToken(string appid, string secret)
        {
            var request = WeChatApiRequestBuilder.GenerateWxTokenRequestUrl(appid, secret);
            return request.GetResponseForJson<WxAccessToken>();
        }


    }
}
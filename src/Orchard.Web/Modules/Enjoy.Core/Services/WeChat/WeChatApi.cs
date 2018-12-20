

namespace Enjoy.Core.Services
{
    using Orchard.Caching;
    using Orchard.Services;
    using Enjoy.Core.EnjoyModels;
    using System;
    using System.Text;
    using System.IO;
    using System.Security.Cryptography;
    using Orchard;
    using Orchard.Logging;
    using Enjoy.Core.WeChatModels;
    using System.Net;
    using System.Security.Cryptography.X509Certificates;
    using System.Net.Security;

    public class WeChatApi : IWeChatApi
    {
        private readonly ICacheManager Cache;
        private readonly IClock Clock;
        private readonly IOrchardServices OS;
        //public const string CacheKey_Token = "Enjoy_WeChat_Token";
        public const string CacheKey_ApplyProtocol = "Enjoy_WeChat_ApplyProtocol";
        public ILogger Logger;
        //private readonly IMerchantService MerchantService;
        public WeChatApi(
            ICacheManager cache,
            IClock clock,
            IOrchardServices os)
        {
            this.Cache = cache;
            this.Clock = clock;
            this.OS = os;
            this.Logger = NullLogger.Instance;
           // this.MerchantService = merchant;
        }
        public string GetToken(string appid, string appsecret)
        {
            return this.Cache.Get(appid, ctx =>
            {
                var token = WeChatApiRequestBuilder.GenerateWxTokenRequestUrl(appid, appsecret).GetResponseForJson<AccessTokenWxResponse>();
                ctx.Monitor(this.Clock.When(TimeSpan.FromSeconds(token.Expiresin)));//默认过期时间为 7200秒
                if (token.HasError) { Logger.Error(token.ErrMsg); }
                return token.Token;
            });
        }
        public string GetToken()
        {
            //var merchant = this.MerchantService.GetDefaultMerchant();
            //if (merchant == null) new NoDefaultMerchantExcpetion();
            
            //if (string.IsNullOrEmpty(merchant.AppId) || string.IsNullOrEmpty(merchant.Secrect)) throw new CheckMerchantException();

            return GetToken(Constants.WxConfig.AppId, Constants.WxConfig.AppSecrect);
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
                string boundary = System.DateTime.UtcNow.Ticks.ToString("X"); // 随机分隔线
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
                this.OS.CreateMediaDirectoryIfNotExits(Constants.Directory_Media_Protocol_ROOT);
                var mediaFileName = this.OS.WorkContext.HttpContext.Server.MapPath(string.Concat(Constants.Directory_Media_Protocol_ROOT, "/", response.MediaId, ".jpg"));
                if (File.Exists(mediaFileName)) File.Delete(mediaFileName);

                using (var stream = new FileStream(mediaFileName, FileMode.Create))
                {
                    stream.Write(buffers, 0, buffers.Length);
                    stream.Flush();
                }
            }
            response.Url = string.IsNullOrEmpty(response.MediaId) ? response.Url : WeChatApiRequestBuilder.GenerateImageUrlByMediaId(response.MediaId);
            return response;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="submerchant"></param>
        /// <returns></returns>
        public WxResponseWapper<CreateSubmerchantResponse> CreateSubmerchant(WxRequestWapper<SubMerchant> submerchant)
        {
            var request = WeChatApiRequestBuilder.GenerateWxSubmerchantUrl(this.GetToken(), submerchant.Info.MerchantId == null);
            return request.GetResponseForJson<WxResponseWapper<CreateSubmerchantResponse>>((http) =>
            {
                http.Method = "POST";
                http.ContentType = "application/json; encoding=utf-8";
                using (var stream = http.GetRequestStream())
                {
                    var body = submerchant.SerializeToJson();
                    var buffers = UTF8Encoding.UTF8.GetBytes(body);
                    stream.Write(buffers, 0, buffers.Length);
                    stream.Flush();
                }
                return http;
            });
        }

        public WxSession CreateWxSession(IWxAuthContext loginUseer)
        {
            var request = WeChatApiRequestBuilder.GenerateWxAuthRequestUrl(Constants.WxConfig.AppId, loginUseer.Code, Constants.WxConfig.AppSecrect);
            var auth = request.GetResponseForJson<WeChatAuthorization>();
            var wechatUser = Decrypt<WeChatUserInfo>(loginUseer.Data, loginUseer.IV, auth.SessionKey);
            return new WxSession() { LoginUser = loginUseer, Miniprogram = Constants.WxConfig, WeCharUser = wechatUser, Authorization = auth };
        }
        public IWxAuthorization GetSessionKey(string code, string appid, string secret)
        {
            var request = WeChatApiRequestBuilder.GenerateWxAuthRequestUrl(appid, code, secret);
            var text = request.GetUriContentDirectly();
            var auth = text.DeserializeToObject<WeChatAuthorization>(); // request.GetResponseForJson<WeChatAuthorization>();
            return auth;
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
        public string GetOpenId(IWxAuthContext loginUser)
        {
            return this.CreateWxSession(loginUser).WeCharUser.OpenId;
        }
        public T Decrypt<T>(string encryptedData, string iv, string sessionKey) where T : class
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
            return result.DeserializeToObject<T>();
        }


        public IWxAuthorization GetWxAuth(IWxAuthContext loginUser)
        {
            throw new NotImplementedException();
        }


        public IWxAccessToken GetWxAccessToken(string appid, string secret)
        {
            var request = WeChatApiRequestBuilder.GenerateWxTokenRequestUrl(appid, secret);
            return request.GetResponseForJson<WxAccessToken>();
        }

        public WeChatUserInfo GetWxUser(string openid)
        {
            var request = WeChatApiRequestBuilder.GenreateQueryWxLoginUserUrl(openid, GetToken());
            var text = request.GetUriContentDirectly();
            var result = request.GetResponseForJson<WeChatUserInfo>();
            return result;
        }
        public WeChatUserInfo GetWxUser(string openid, string appid, string secret)
        {
            var request = WeChatApiRequestBuilder.GenreateQueryWxLoginUserUrl(openid, GetToken(appid, secret));
            var text = request.GetUriContentDirectly();
            var result = request.GetResponseForJson<WeChatUserInfo>();

            return result;
        }
        public void CheckCardAgentQulification()
        {
            var request = WeChatApiRequestBuilder.GenerateCheckCardAgentRequest(GetToken());
            var context = request.GetUriContentDirectly();
        }

        public NormalWxResponse DeleteCardCoupon(string cardid)
        {
            var request = WeChatApiRequestBuilder.GenerateDeleteCardCoupon(GetToken());
            return request.GetResponseForJson<NormalWxResponse>((http) =>
            {
                var data = new { card_id = cardid };
                http.Method = "POST";
                http.ContentType = "application/json; encoding=utf-8";
                using (var stream = http.GetRequestStream())
                {
                    var body = data.SerializeToJson();
                    var buffers = UTF8Encoding.UTF8.GetBytes(body);
                    stream.Write(buffers, 0, buffers.Length);
                    stream.Flush();
                }
                return http;
            });
        }
        public QueryCardCouponWxResponse QueryCardCouponOnWechat()
        {
            var request = WeChatApiRequestBuilder.GenerateQueryCardUrl(GetToken());
            //支持开发者拉出指定状态的卡券列表 
            //CARD_STATUS_NOT_VERIFY”, 待审核 ； 
            //“CARD_STATUS_VERIFY_FAIL”, 审核失败； 
            //“CARD_STATUS_VERIFY_OK”， 通过审核； 
            //“CARD_STATUS_DELETE”， 卡券被商户删除； 
            //“CARD_STATUS_DISPATCH”， 在公众平台投放过的卡券；
            return request.GetResponseForJson<QueryCardCouponWxResponse>((http) =>
             {
                 var data = new
                 {
                     offset = 0,
                     count = 100,
                     status_list = new string[]
                 {
                    "CARD_STATUS_NOT_VERIFY","CARD_STATUS_VERIFY_FAIL","CARD_STATUS_VERIFY_OK","CARD_STATUS_DELETE","CARD_STATUS_DISPATCH"
                 }
                 };
                 http.Method = "POST";
                 http.ContentType = "application/json; encoding=utf-8";
                 using (var stream = http.GetRequestStream())
                 {
                     var body = data.SerializeToJson();
                     var buffers = UTF8Encoding.UTF8.GetBytes(body);
                     stream.Write(buffers, 0, buffers.Length);
                     stream.Flush();
                 }
                 return http;
             });
        }
        public void SetMemberCardFieldIfActiveByWx(string cardid)
        {
            var request = WeChatApiRequestBuilder.GenerateMemberActiveUserform(GetToken());
            var result = request.GetResponseForJson<NormalWxResponse>((http) =>
              {
                  var data = new
                  {
                      card_id = cardid,
                      //service_statement = new
                      //{
                      //    name = "会员守则",
                      //    url = "https://www.yourc.club/wap/statement"////TODO :需要完成 会员守则页面
                      //},
                      //bind_old_card = new
                      //{
                      //    name = "老会员绑定",
                      //    url = "https =//www.qq.com"
                      //},
                      required_form = new
                      {
                          can_modify = false,
                          common_field_id_list = new string[] { "USER_FORM_INFO_FLAG_MOBILE" }
                      },
                      optional_form = new
                      {
                          can_modify = false,
                          common_field_id_list = new string[] {
                            "USER_FORM_INFO_FLAG_NAME",
                            "USER_FORM_INFO_FLAG_SEX",
                            "USER_FORM_INFO_FLAG_BIRTHDAY" }
                      }
                  };
                  http.Method = "POST";
                  http.ContentType = "application/json; encoding=utf-8";

                  using (var stream = http.GetRequestStream())
                  {
                      var body = data.SerializeToJson();
                      var buffers = UTF8Encoding.UTF8.GetBytes(body);
                      stream.Write(buffers, 0, buffers.Length);
                      stream.Flush();
                  }
                  return http;
              });

            /*
             * 字段值	描述
            USER_FORM_INFO_FLAG_MOBILE	手机号
            USER_FORM_INFO_FLAG_SEX	性别
            USER_FORM_INFO_FLAG_NAME	姓名
            USER_FORM_INFO_FLAG_BIRTHDAY	生日
            USER_FORM_INFO_FLAG_IDCARD	身份证
            USER_FORM_INFO_FLAG_EMAIL	邮箱
            USER_FORM_INFO_FLAG_LOCATION	详细地址
            USER_FORM_INFO_FLAG_EDUCATION_BACKGRO	教育背景
            USER_FORM_INFO_FLAG_INDUSTRY	行业
            USER_FORM_INFO_FLAG_INCOME	收入
            USER_FORM_INFO_FLAG_HABIT	兴趣爱好
             */
        }

        public WxAccessToken GetAccessTokenByCode(string code)
        {
            var request = WeChatApiRequestBuilder.GenerateOAuth2ByCode(code);
            var token = request.GetResponseForJson<WxAccessToken>();
            //根据openid 换回 unionid以及其他用户信息
            var queryLoginUser = WeChatApiRequestBuilder.GenreateQueryWxLoginUserUrl(token.OpenId, GetToken());
            token.LoginUser = queryLoginUser.GetResponseForJson<WxLoginUser>();
            return token;
        }

        public WxPayParameter Unifiedorder(JsApiPay jsApiPay)
        {
            var input = jsApiPay.GenerateUnifiedWxPayData();
            var request = "https://api.mch.weixin.qq.com/pay/unifiedorder";
            var order = request.GetUriContentDirectly((http) =>
            {
                if (request.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                            new RemoteCertificateValidationCallback(CheckValidationResult);
                }
                http.Timeout = 30 * 1000;
                ServicePointManager.DefaultConnectionLimit = 200;
                http.UserAgent = string.Format("WXPaySDK/{3} ({0}) .net/{1} {2}",
                    Environment.OSVersion, Environment.Version, Constants.WxConfig.MchId,
                    typeof(WxPayParameter).Assembly.GetName().Version);
                http.Method = "POST";
                http.ContentType = "text/xml";
                using (var stream = http.GetRequestStream())
                {
                    var body = input.SerializeToXml();
                    var buffers = UTF8Encoding.UTF8.GetBytes(body);
                    stream.Write(buffers, 0, buffers.Length);
                    stream.Flush();
                }
                return http;
            }).DeserializeFromXml<WxUnifiedorderResponse>();
            return new WxPayParameter(order);
        }
        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            //直接确认，否则打不开    
            return true;
        }


    }
}



namespace Enjoy.Core.Api
{
    using System.Web.Http;
    using Orchard;
    using Enjoy.Core.Models;
    using Orchard.Logging;
    using Enjoy.Core;
    using System.IO;

    //[Authorize]
    public class EnjoyController : ApiController
    {
        private readonly IEnjoyAuthService Auth;
        private readonly IMerchantService Merchant;
        private readonly IOrchardServices OS;
        private readonly IWeChatApi WeChat;
        private readonly IWeChatEventBehavior Behavior;
        public EnjoyController(
            IOrchardServices os,
            IEnjoyAuthService auth,
            IWeChatApi wechat,
            IMerchantService merchant,
            IWeChatEventBehavior behavior)
        {
            this.Auth = auth;
            this.OS = os;
            this.Merchant = merchant;
            this.WeChat = wechat;
            this.Logger = NullLogger.Instance;
            this.Behavior = behavior;
        }
        public ILogger Logger { get; set; }
        [HttpGet]
        public AuthQueryResponse GetEnjoyUser(string mobile)
        {
            return this.Auth.QueryByMobile(mobile);
        }
        [Route("api/enjoy/decode")]
        [HttpGet]
        public WxSession DecodeUserinfo(string code, string iv, string encryptedData, string signature)
        {
            return this.WeChat.CreateWxSession(new WxLoginUser(code, iv, encryptedData, signature));
        }

        //[Route("api/enjoy/test")]
        //[HttpGet]
        //public IMiniprogram Test()
        //{
        //    Logger.Error("Tesxxx");
        //    return EnjoyConstant.Miniprogram;
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="token"></param>
        /// <param name="arg"></param>
        /// <remarks>
        /// 参数	描述
        ///signature 微信加密签名，signature结合了开发者填写的token参数和请求中的timestamp参数、nonce参数。
        ///timestamp 时间戳
        ///nonce 随机数
        ///echostr 随机字符串
        /// </remarks>
        [Route("api/enjoy/WxBizMsg")]
        [HttpPost]
        [HttpGet]
        public void WxBizMsg(string signature = null, string timestamp = null, string nonce = null, string echostr = null)
        {
            if (this.OS.WorkContext.HttpContext.Request.HttpMethod.Equals("GET", System.StringComparison.OrdinalIgnoreCase))
            {
                this.OS.WorkContext.HttpContext.Response.Write(echostr);
                this.OS.WorkContext.HttpContext.Response.End();
                return;
            }
            var token = new WxMsgToken(signature,
                timestamp,
                nonce,
                ReadStream2String(this.OS.WorkContext.HttpContext.Request.InputStream));
            Logger.Error(token.ToJson());
            this.Behavior.Execute(token);
        }
        private string ReadStream2String(Stream stream)
        {
            if (null == stream)
            {
                return string.Empty;
            }
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}


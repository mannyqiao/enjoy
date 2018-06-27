


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
        public EnjoyController(
            IOrchardServices os,
            IEnjoyAuthService auth,
            IWeChatApi wechat,
            IMerchantService merchant)
        {
            this.Auth = auth;
            this.OS = os;
            this.Merchant = merchant;
            this.WeChat = wechat;
            this.Logger = NullLogger.Instance;
        }
        public ILogger Logger { get; set; }
        [HttpGet]
        public EnjoyUserProfile GetEnjoyUser(string mobile)
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
            //公众平台上开发者设置的token, appID, EncodingAESKey   
            // Token:           EnjoyMini
            // EncodingAESKey:  2f0utlUlEJCGJmpwGYDmX184OZpLGrHj7EXG2ynyThC         
            //If get that's meaning you are setting receive  server.
            if (this.OS.WorkContext.HttpContext.Request.HttpMethod.Equals("GET", System.StringComparison.OrdinalIgnoreCase))
            {
                this.OS.WorkContext.HttpContext.Response.Write(echostr);
                this.OS.WorkContext.HttpContext.Response.End();
                return;
            }
            var token = "EnjoyMini";
            var encodingAESKey = "2f0utlUlEJCGJmpwGYDmX184OZpLGrHj7EXG2ynyThC";
            var sReqData = ReadStream2String(this.OS.WorkContext.HttpContext.Request.InputStream);
            Logger.Error((new
            {
                signature = signature,
                timestamp = timestamp,
                nonce = nonce
            }).ToJson());

            Logger.Error(sReqData);
            //string sToken = "EnjoyVip";
            //string sAppID = EnjoyConstant.Miniprogram.AppId;// "wx5823bf96d3bd56c7";
            //string sEncodingAESKey = "2f0utlUlEJCGJmpwGYDmX184OZpLGrHj7EXG2ynyThC";
            //WXBizMsgCrypt crypt = new WXBizMsgCrypt(sToken, sEncodingAESKey, sAppID);
            //var sReqData = ReadStream2String(this.OS.WorkContext.HttpContext.Request.InputStream);
            //this.Logger.Error("ReqData:\r\n" + sReqData);
            //var sDecryptText = string.Empty;
            //var ret = crypt.DecryptMsg(signature, timestamp, nonce, sReqData, ref sDecryptText);
            //if (ret != 0)
            //{
            //    this.OS.WorkContext.HttpContext.Response.Write("decrypt fail");
            //    this.OS.WorkContext.HttpContext.Response.End();
            //    return;
            //}

            //this.Logger.Error("sDecryptText:\r\n" + sDecryptText);
            //this.OS.WorkContext.HttpContext.Response.Write(echostr);
            //this.OS.WorkContext.HttpContext.Response.End();
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


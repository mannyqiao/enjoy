


namespace Enjoy.Core.Api
{
    using System.Web.Http;
    using Orchard;
    using Enjoy.Core.Models;
    using Orchard.Logging;
    using Enjoy.Core;
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

        [Route("api/enjoy/test")]
        [HttpGet]
        public IMiniprogram Test()
        {
            Logger.Error("Tesxxx");
            return EnjoyConstant.Miniprogram;
        }
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
        [HttpGet]
        [HttpPost]
        public void WxBizMsg(string signature, long timestamp, decimal nonce, string echostr)
        {
            //https://www.yourc.club/api/enjoy/WxBizMsg/?signature=2dacca8c007705eb0405b30e3fc1ac65d62637ed&echostr=15107807748897636285&timestamp=1529048324&nonce=1809598692
            //https://www.yourc.club/api/enjoy/WxBizMsg?signature=a6b21563f68a6f8042a81363799ff9f2f5656055&echostr=7077494231949635310&timestamp=1529048742&nonce=1832270831
            //echostr: 7077494231949635310 
            //公众平台上开发者设置的token, appID, EncodingAESKey
            string sToken = "EnjoyVip";
            string sAppID = EnjoyConstant.Miniprogram.AppId;// "wx5823bf96d3bd56c7";
            string sEncodingAESKey = "YRNr812O9yoWbhCpR3YkvHiz7YtYg894pqbodbOUBoT";
            WXBizMsgCrypt crypt = new WXBizMsgCrypt(sToken, sEncodingAESKey, sAppID);

            //var body = (string)arg.ToJson();
            Logger.Error(string.Concat("From EventHandler ", signature, ",",timestamp.ToDateTimeFromUnixStamp().ToString("yyyy-MM-dd HH:mm")));
            //Logger.Error(this.Request.ToJson());
            Logger.Error("echost:" + echostr);
        }       
    }
}


/*
 * 小程序客户端api
 */


namespace Enjoy.Core.Api
{
    using System.Web.Http;
    using Orchard;
    using Enjoy.Core.EnjoyModels;
    using Orchard.Logging;
    using Enjoy.Core;

    using Enjoy.Core.WeChatModels;
    using System.Collections.Generic;
    using Enjoy.Core.ApiModels;
    using System.Linq;

    //[Authorize]
    public class EnjoyController : ApiController
    {
        private readonly IEnjoyAuthService Auth;
        private readonly IMerchantService Merchant;
        private readonly IOrchardServices OS;
        private readonly IWeChatApi WeChat;
        private readonly IWeChatMsgHandler _handler;
        public EnjoyController(
            IOrchardServices os,
            IEnjoyAuthService auth,
            IWeChatApi wechat,
            IMerchantService merchant,
            IWeChatMsgHandler handler)
        {
            this.Auth = auth;
            this.OS = os;
            this.Merchant = merchant;
            this.WeChat = wechat;
            this.Logger = NullLogger.Instance;
            this._handler = handler;
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
        // 微信POST 示例 api/enjoy/WxBizMsg?signature=42960d8b94eeac9c28f01bfada5b36f8afb86538
        //&timestamp=1533011632
        //&nonce=117779779
        //&openid=o8ntH0r7gF7MRjz0-m8JpuFhpmEw
        //&encrypt_type=aes
        //&msg_signature=31afefd9f0302c1395ae23ba5d68a7de8562c369
        /// </remarks>
        [Route("api/enjoy/WxBizMsg")]
        [HttpPost]
        [HttpGet]
        public void WxBizMsg(
            string signature = null,
            string timestamp = null,
            string nonce = null,
            string openid = null,
            string encrypt_type = null,
            string msg_signature = null,
            string echostr = null)
        {

            if (this.OS.WorkContext.HttpContext.Request.HttpMethod.Equals("GET", System.StringComparison.OrdinalIgnoreCase))
            {
                this.OS.WorkContext.HttpContext.Response.Write(echostr);
                this.OS.WorkContext.HttpContext.Response.End();
                return;
            }
            var token = new WxMsgToken(msg_signature,
                timestamp,
                nonce,
                this.OS.WorkContext.HttpContext.Request.InputStream.ReadStream());
            string weChatMsg = string.Concat(
                string.Format("requestUrl:{0}\r\n", this.OS.WorkContext.HttpContext.Request.RawUrl),
                string.Format("xmlbody = {0} \r\n", token.ReqMsg)
            );
            Logger.Error(weChatMsg);
            this._handler.Handle(token);

        }

        [Route("api/enjoy/QueryMerchants")]
        [HttpPost]
        [HttpGet]
        public List<Banner> QueryMerchants(int page = 1, int size = 10)
        {
            var condition = PagingCondition.GenerateByPageAndSize(page, size);
            return this.Merchant.QueryMerchants(new QueryFilter()
            {
                Columns = new List<QueryColumnFilter>()
                {
                    new QueryColumnFilter(){
                        Name ="Status",
                        Searchable = true,
                        Search = new SearchColumnFilter(){
                             Value = AuditStatus.APPROVED
                        },
                        Orderable = true,
                        Data = "Status"
                    }
                },
                Order = new List<QueryOrderFilter>()
                {
                    new QueryOrderFilter()
                    {
                        ColumnName = "CreateTime",
                        Dir =  Direction.Asc
                    }
                }
            }, condition)
            .Items
            .Select((ctx) =>
            {
                return new Banner()
                {
                    LinkName = ctx.BrandName,
                    LinkTo = string.Empty,
                    LogoUrl = ctx.LogoUrl,
                    LinkType = 9
                };
            }).ToList();
        }
    }
}


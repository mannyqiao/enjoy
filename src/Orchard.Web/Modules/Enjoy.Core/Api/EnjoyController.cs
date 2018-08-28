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
    using Enjoy.Core.ViewModels;
    using System;
    //[Authorize]
    public class EnjoyController : ApiController
    {
        private readonly IEnjoyAuthService _authService;
        private readonly IMerchantService _merchantService;
        private readonly IOrchardServices _os;
        private readonly IWeChatApi _weChat;
        private readonly IWeChatMsgHandler _handler;
        private readonly IWxUserService _wxUserService;
        private readonly IShopService _shopservice;
        public EnjoyController(
            IOrchardServices os,
            IEnjoyAuthService auth,
            IWeChatApi wechat,
            IMerchantService merchant,
            IWxUserService wxUserService,
            IShopService shop,
            IWeChatMsgHandler handler)
        {
            this._authService = auth;
            this._os = os;
            this._merchantService = merchant;
            this._weChat = wechat;
            this.Logger = NullLogger.Instance;
            this._handler = handler;
            this._wxUserService = wxUserService;
            this._shopservice = shop;

        }
        public ILogger Logger { get; set; }
        [HttpGet]
        public AuthQueryResponse GetEnjoyUser(string mobile)
        {
            return this._authService.QueryByMobile(mobile);
        }

        [Route("api/enjoy/GetSessionKey")]
        [HttpPost]
        public IWxAuthorization GetSessionKey(JSCodeContext signature)
        {
            //var text = this.OS.WorkContext.HttpContext.Request.InputStream.ReadStream();
            var result = this._weChat.GetSessionKey(signature.Code, signature.AppId, signature.Secret);
            return result;
        }
        [Route("api/enjoy/DecryptUserInfo")]
        [HttpPost]
        public WeChatUserInfo DecryptUserInfo(DecryptContext context)
        {
            var result = this._weChat.Decrypt(context.Data, context.IV, context.SessionKey);
            //检查用户状态
            var wxuser = this._wxUserService.GetWxUser(result.UnionId);
            if (wxuser == null)
            {
                wxuser = new WxUserModel()
                {
                    City = result.City,
                    Country = result.Country,
                    CreatedTime = DateTime.Now.ToUnixStampDateTime(),
                    LastActiveTime = DateTime.Now.ToUnixStampDateTime(),
                    Mobile = string.Empty,
                    NickName = result.NickName,
                    Province = result.Province,
                    RegistryType = RegistryTypes.Miniprogram,
                    UnionId = result.UnionId
                };
                result.State = new UserState() { HasMobile = false, Signup = true };
                this._wxUserService.Register(wxuser);
            }
            else
            {
                result.State = new UserState() { HasMobile = !string.IsNullOrEmpty(wxuser.Mobile), Signup = true };
            }
            result.Id = wxuser.Id;
            return result;
        }

        //[Route("api/enjoy/signature")]
        //[HttpGet]
        //public WxSession DecodeUserinfo(
        //    string appid,
        //    string secret,
        //    string code,
        //    string grant_type)
        //{
        //    return this.WeChat.CreateWxSession(new WxLoginUser(code, iv, encryptedData, signature));
        //}

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

            if (this._os.WorkContext.HttpContext.Request.HttpMethod.Equals("GET", System.StringComparison.OrdinalIgnoreCase))
            {
                this._os.WorkContext.HttpContext.Response.Write(echostr);
                this._os.WorkContext.HttpContext.Response.End();
                return;
            }
            var token = new WxMsgToken(msg_signature,
                timestamp,
                nonce,
                this._os.WorkContext.HttpContext.Request.InputStream.ReadStream());
            string weChatMsg = string.Concat(
                string.Format("requestUrl:{0}\r\n", this._os.WorkContext.HttpContext.Request.RawUrl),
                string.Format("xmlbody = {0} \r\n", token.ReqMsg)
            );
            Logger.Error(weChatMsg);
            this._handler.Handle(token);

        }

        [Route("api/enjoy/QueryMerchants")]
        [HttpPost]
        public List<Banner> QueryMerchants(PagingX paging)
        {
            var condition = PagingCondition.GenerateByPageAndSize(paging.Page, paging.PageSize);
            return this._merchantService.QueryMerchants(new QueryFilter()
            {
                Columns = new List<QueryColumnFilter>()
                {
                    ////TODO 正式版本中需要取消下面这段代码的注释
                    //new QueryColumnFilter(){
                    //    Name ="Status",
                    //    Searchable = true,
                    //     DbType = System.Data.DbType.String,
                    //    Search = new SearchColumnFilter(){
                    //         Value = AuditStatus.APPROVED
                    //    },
                    //    Orderable = true,
                    //    Data = "Status"
                    //}
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


        [Route("api/enjoy/sendverifycode")]
        [HttpPost]
        public ActionResponse<VerificationCodeViewModel> SendVerifyCode(Mobile mobile)
        {
            return this._authService.GetverificationCode(mobile.Value, VerifyTypes.BindWeChatUser);
        }
        [Route("api/enjoy/QueryShops")]
        [HttpPost]
        public List<ShopNearyby> QueryShops(Location location)
        {
            var paging = PagingCondition.GenerateByPageAndSize(1, 15);
            return this._shopservice.QueryShops(null, paging)
                .Items.Select((ctx) =>
                {
                    var lac = ctx.Coordinate.DeserializeToObject<Location>();
                    return new ShopNearyby()
                    {
                        Lat = lac.Latitude,
                        Lng = lac.Longitude,
                        ShopActs = new ShopAct[] { },
                        ShopAddress = ctx.Address,
                        ShopId = ctx.Id,
                        ShopLogo = ctx.Merchant.LogoUrl,
                        ShopName = string.Format("{0}[{1}]", ctx.Merchant.BrandName, ctx.ShopName)
                    };
                }).ToList();
        }
    }
}


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
    using ApiModel = Enjoy.Core.ApiModels;
    using System.Linq;
    using Enjoy.Core.ViewModels;
    using System;
    using Enjoy.Core.Services;
    using Enjoy.Core.Records;

    using System.Text;
    using NHibernate.Linq;
    using NHibernate.Criterion;

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
        private readonly ICardCouponService _cardCouponService;
        public EnjoyController(
            IOrchardServices os,
            IEnjoyAuthService auth,
            IWeChatApi wechat,
            IMerchantService merchant,
            IWxUserService wxUserService,
            IShopService shop,
            ICardCouponService cardCoupon,
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
            this._cardCouponService = cardCoupon;

        }
        public ILogger Logger { get; set; }
        [HttpGet]
        public AuthQueryResponse GetEnjoyUser(string mobile)
        {
            return this._authService.QueryByMobile(mobile);
        }

        [Route("api/enjoy/GetSessionKey")]
        [HttpPost]
        public IWxAuthorization GetSessionKey(ApiModel::JSCodeContext signature)
        {
            //var text = this.OS.WorkContext.HttpContext.Request.InputStream.ReadStream();
            var result = this._weChat.GetSessionKey(signature.Code, signature.AppId, signature.Secret);
            return result;
        }
        [Route("api/enjoy/DecryptUserInfo")]
        [HttpPost]
        public WeChatUserInfo DecryptUserInfo(ApiModel::DecryptContext context)
        {
            var result = this._weChat.Decrypt<WeChatUserInfo>(context.Data, context.IV, context.SessionKey);
            //检查用户状态
            var wxuser = this._wxUserService.GetWxUser(context.AppId, result.OpenId);
            if (wxuser == null)
            {
                wxuser = result.CreateWxUser(RegistryTypes.Miniprogram, context.AppId);
                this._os.TransactionManager.GetSession().SaveOrUpdate(wxuser);
            }
            else
            {
                result.State = new ApiModel::UserState() { HasMobile = !string.IsNullOrEmpty(wxuser.Mobile), Signup = true };
                wxuser.LastActivityTime = DateTime.Now.ToUnixStampDateTime();
                this._os.TransactionManager.GetSession().SaveOrUpdate(wxuser);
            }
            result.Id = wxuser.Id;
            result.Mobile = wxuser.Mobile;
            return result;
        }

        /// <summary>
        /// 检查验证码是否正确
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="verifyCode"></param>
        /// <returns></returns>
        [Route("api/enjoy/CheckVerifyCode")]
        [HttpPost]
        public bool CheckVerifyCode(ApiModel::VerifyCodeContext context)
        {
            return this._authService.IsEquals(context.Mobile, context.VerifyCode);
        }


        [Route("api/enjoy/BindMobile")]
        [HttpPost]
        public dynamic BindMobile(ApiModel::DecryptContext context)
        {
            var result = this._weChat.Decrypt<PhoneNumberWxResponse>(context.Data, context.IV, context.SessionKey);
            var model = this._wxUserService.GetWxUser(context.WxChatUser.UnionId);
            if (model != null)
            {
                model.Mobile = result.PhoneNumber;
                this._wxUserService.Register(model);
            }
            return new
            {
                state = new { hasMobile = true },
                mobile = model.Mobile
            };
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
        [Obsolete("This api has been deprecated")]
        [Route("api/enjoy/QueryMerchants")]
        [HttpPost]
        public List<ApiModel::Banner> QueryMerchants(ApiModel::PagingX paging)
        {
            var condition = PagingCondition.GenerateByPageAndSize(paging.Page, paging.PageSize);
            return this._merchantService.QueryMerchants(new WebQueryFilter()
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
                return new ApiModel::Banner()
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
        public ActionResponse<VerificationCodeViewModel> SendVerifyCode(ApiModel::Mobile mobile)
        {
            return this._authService.GetverificationCode(mobile.Value, VerifyTypes.BindWeChatUser);
        }

        /// <summary>
        /// 查询推荐的会员卡
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        [Route("api/enjoy/QueryCards")]
        [HttpPost]
        public List<ApiModel::CardCoupon> QueryCards(ApiModel::QueryCardsContext context)
        {
            var data = this._cardCouponService.QueryCardCoupon(
                context.MerchantId, context.Types, new CardCouponStates[] { CardCouponStates.Approved });
            return data.Select(o => new ApiModel::CardCoupon()
            {
                BrandName = o.BrandName,
                Id = o.Id,
                Mid = o.Merchant.Id,
                WxNo = o.WxNo,
                LogoUrl = o.CardCoupon.BaseInfo.LogoUrl,
                MerchantName = o.Merchant.BrandName,
                Privilege = (o.CardCoupon as MemberCard).Prerogative,
            }).ToList();
        }

        [Route("api/enjoy/QueryCardById")]
        [HttpPost]
        public ApiModel::CardCoupon QueryCardById(long id)
        {
            return new ApiModel::CardCoupon();
        }


        [Route("api/enjoy/GenerateCardExtString")]
        [HttpPost]
        public string GenerateCardExtString(ApiModel::SignatureContext context)
        {
            var timestamp = DateTime.Now.ToUnixStampDateTime();
            var nonce = Guid.NewGuid().ToString().Replace("-", string.Empty);
            var result = new ApiModel::CardExtModel()
            {
                //OpenId = context.OpenId,
                Signature = this._weChat.GenerateCardSignature(context.AppId, context.AppSecret, context.CardId, timestamp, nonce),
                TimeStamp = timestamp.ToString(),
                NonceStr = nonce,
                OuterStr = "Miniprogram"
            };
            return result.SerializeToJson();
        }
        /// <summary>
        /// 充值统一下单
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [Route("api/enjoy/GenerateUnifiedorderforTopup")]
        [HttpPost]
        public ApiModel::PullWxPayData GenerateUnifiedorderforTopup(ApiModel::TopupContext context)
        {
            var merchant = this._merchantService.GetDefaultMerchant(context.MCode);
            if (merchant == null) { throw new NullReferenceException("mcode"); }
            var session = this._os.TransactionManager.GetSession();
            //创建本地支付订单
            var trade = context.Generate(merchant.Miniprogram.MchId);
            session.SaveOrUpdate(trade);
            trade.TradeId = string.Format("T{0}{1}", DateTime.Now.ToString("yyyyMMddmmddss"), trade.Id.ToString("00000000"));
            var data = context.GenerateUnifiedWxPayData(merchant.Miniprogram.MchId, trade.TradeId, merchant.Miniprogram.PayKey);
            var parameter = this._weChat.Unifiedorder(data);
            parameter.PaySign = parameter.MakeSign();
            var pullWxPayData = new ApiModel::PullWxPayData()
            {
                nonceStr = parameter.NonceStr,
                package = parameter.Package,
                paySign = parameter.PaySign,
                signType = WxPayData.SIGN_TYPE_HMAC_SHA256,
                timeStamp = parameter.TimeStamp.ToString()
            };
            return pullWxPayData;
        }

        /// <summary>
        /// 创建分享日志
        /// </summary>
        /// <param name="context"></param>
        [Route("api/enjoy/CreateShareLogging")]
        [HttpPost]
        public void CreateShareLogging(ApiModel::SharingContext context)
        {
            var merchant = this._merchantService.GetDefaultMerchant(context.MCode);
            var session = this._os.TransactionManager.GetSession();
            session.SaveOrUpdate(new SharingDetails()
            {
                AppId = context.AppId,
                CardId = context.CardId,
                CreatedTime = DateTime.Now.ToUnixStampDateTime(),
                Merchant = new Merchant() { Id = merchant.Id },
                SharedBy = context.SharedBy
            });
        }

        [Route("api/enjoy/PayNotify")]
        [HttpGet]
        [HttpPost]
        public void PayNotify()
        {
            var xml = this.Request.Content.ReadAsStringAsync();
            var notify = xml.Result.DeserializeFromXml<PayNotification>();
            if (notify == null) throw new NullReferenceException("nofity");
            var session = this._os.TransactionManager.GetSession();
            var criteria = session.CreateCriteria<TradeDetails>();
            criteria.Add(Expression.Eq("AppId", notify.AppId.Value));
            criteria.Add(Expression.Eq("TradeId", notify.OutTradeNo.Value));
            var trade = criteria.UniqueResult<TradeDetails>();
            if (notify.TotalFee != trade.Money)
                throw new ArgumentNullException("trade money not equal notify money");
            if (trade.State != TradeStates.Waiting)
            {
                var str = @"<xml>
                  <return_code><![CDATA[Fail]]></return_code>
                  <return_msg><![CDATA[NO]]></return_msg>
                </xml>";
                this._os.WorkContext.HttpContext.Response.Write(str);
                this._os.WorkContext.HttpContext.Response.End();
                return;
            }



            ////P1 需要加入签名验证防止数据篡改
            if (notify.ReturnCode.Value.Equals("SUCCESS"))
            {
                trade.State = TradeStates.Success;
                trade.OrderId = notify.TransactionId.Value;
                trade.Description = notify.Attach.Value;
                session.SaveOrUpdate(trade);
                var va = this._authService.CreateVirtualAccountIfNotExists(trade);
                va.Money += trade.RealMoeny;
                va.LastTrading = trade;
                session.SaveOrUpdate(va);
                var str = @"<xml>
                  <return_code><![CDATA[SUCCESS]]></return_code>
                  <return_msg><![CDATA[OK]]></return_msg>
                </xml>";
                this._os.WorkContext.HttpContext.Response.Write(str);
                this._os.WorkContext.HttpContext.Response.End();
            }
            else
            {
                trade.State = TradeStates.Cancel;
                trade.OrderId = notify.TransactionId.Value;
                trade.Description = string.Format("attach:{0};error:{1};", notify.Attach.Value, notify.ErrorDescription.Value);
                session.SaveOrUpdate(trade);
                var str = @"<xml>
                  <return_code><![CDATA[Fail]]></return_code>
                  <return_msg><![CDATA[NO]]></return_msg>
                </xml>";
                this._os.WorkContext.HttpContext.Response.Write(str);
                this._os.WorkContext.HttpContext.Response.End();
            }



        }

    }
}


﻿
namespace Enjoy.Core.Controllers
{
    using Orchard.Themes;
    using System.Web.Mvc;
    using Orchard;
    using Orchard.Mvc.Extensions;
    using Enjoy.Core.WeChatModels;
    using System.Web;
    using Orchard.Logging;

    [Themed]
    public class WAPController : Controller
    {
        private readonly IWeChatApi _weChatApi;
        private readonly IOrchardServices _os;
        public ILogger Logger;
        public WAPController(IWeChatApi weChatApi, IOrchardServices os)
        {
            this._weChatApi = weChatApi;
            this._os = os;
            Logger = NullLogger.Instance;
        }
        /// <summary>
        /// 充值
        /// </summary>
        /// <returns></returns>
        public ActionResult Topup()
        {
            return View();
        }
        /// <summary>
        /// 支付
        /// </summary>
        /// <returns></returns>
        public ActionResult Pay(string code, string state)
        {

            var token = this._weChatApi.GetAccessTokenByCode(code);
            var jsApiData = new JsApiPay()
            {
                OpenId = token.OpenId,
                TotalFee = null,
                UnionId = token.LoginUser.Unionid
            };
            //根据Code 获取OpenId
            return View(jsApiData);
        }
        [HttpPost]
        public ActionResult PayPost(JsApiPay data)
        {
            //var wxPayParameter = this._weChatApi.Unifiedorder(data);
            
            //var url = string.Format("/wap/pullwxpay?appid={0}&timestamp={1}&noncestr={2}&package={3}&signType={4}&paySign={5}&ReturnMsg={6}"
            //    , wxPayParameter.AppId
            //    , wxPayParameter.TimeStamp
            //    , wxPayParameter.NonceStr
            //    , wxPayParameter.Package
            //    , wxPayParameter.SignType
            //    , wxPayParameter.PaySign
            //    ,wxPayParameter.ReturnMsg);
            return this.RedirectLocal("");
        }
        /// <summary>
        /// 接收支付结果
        /// </summary>
        /// <returns></returns>
        public ActionResult Payr(string error)
        {
            var response = new NormalWxResponse() { ErrMsg = error };
            return View(response);
        }
        /// <summary>
        /// 拉起微信支付
        /// </summary>
        /// <returns></returns>
        public ActionResult PullWxPay(WxPayParameter parameter)
        {            
            parameter.PaySign = parameter.MakeSign();
            return View(parameter);
        }
        /// <summary>
        /// 查看余额
        /// </summary>
        /// <returns></returns>
        public ActionResult Balance(string code)
        {
            return View();
        }
        public ActionResult WxDirect(string direct)
        {
            return View();
        }
    }
}
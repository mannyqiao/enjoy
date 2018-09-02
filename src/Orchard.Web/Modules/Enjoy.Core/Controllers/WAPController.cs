
namespace Enjoy.Core.Controllers
{
    using Orchard.Themes;
    using System.Web.Mvc;
    using Orchard;
    using Orchard.Mvc.Extensions;
    using Enjoy.Core.WeChatModels;
    using System.Web;
    [Themed]
    public class WAPController : Controller
    {
        private readonly IWeChatApi _weChatApi;
        private readonly IOrchardServices _os;
        public WAPController(IWeChatApi weChatApi, IOrchardServices os)
        {
            this._weChatApi = weChatApi;
            this._os = os;
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
            var text = this._weChatApi.JsPay(data);
            return this.RedirectLocal(string.Format("/wap/payr?text={0}", HttpUtility.UrlEncode(text)));
        }
        /// <summary>
        /// 接收支付结果
        /// </summary>
        /// <returns></returns>
        public ActionResult Payr(string text)
        {
            this._os.WorkContext.HttpContext.Response.Write(HttpUtility.UrlDecode(text));
            return View();
        }
    }
}
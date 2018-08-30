
namespace Enjoy.Core.Controllers
{
    using Orchard.Themes;
    using System.Web.Mvc;
    using Orchard;
    using Orchard.Mvc.Extensions;
    [Themed]
    public class WAPController : Controller
    {
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
        public ActionResult Pay()
        {
            return View();
        }
    }
}
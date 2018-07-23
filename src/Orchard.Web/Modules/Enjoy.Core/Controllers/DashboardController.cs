
namespace Enjoy.Core.Controllers
{
    using Orchard.Themes;
    using System.Web.Mvc;
    using Orchard;
    using Orchard.Mvc.Extensions;
    [Themed]
    public class DashboardController : Controller
    {
        private readonly IWeChatApi WeChat;
        private readonly IOrchardServices OS;
        public DashboardController(IOrchardServices os, IWeChatApi api)
        {
            this.WeChat = api;
            this.OS = os;
        }
        // GET: Dashboard
        public ActionResult Summary()
        {
            //"wx0c644f8027d78c74", "f1681068dfcd75ef2d7dff14cb3b5fae"
            //if (this.OS.WorkContext.GetState<IEnjoyUser>(EnjoyConstant.EnjoyCurrentUser) == null)
            //    return this.RedirectLocal("/access/sign?signin=true");
            return View();
        }
    }
}
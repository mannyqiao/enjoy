
namespace Enjoy.Core.Controllers
{
    using Orchard.Themes;
    using System.Web.Mvc;
    using Orchard;
    using Orchard.Mvc.Extensions;
    [Themed]
    public class DashboardController : Controller
    {

        private readonly IEnjoyAuthService Auth;
        public DashboardController(IEnjoyAuthService auth)
        {
            this.Auth = auth;
        }
        // GET: Dashboard
        public ActionResult Summary()
        {
            if (this.Auth.GetAuthenticatedUser() == null)
                return this.RedirectLocal("/access/sign?signin=true");
            return View();
        }
    }
}
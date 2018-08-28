
namespace Enjoy.Core.Controllers
{
    using Orchard.Themes;
    using System.Web.Mvc;
    using Orchard;
    using Orchard.Mvc.Extensions;
    [Themed]
    public class WAPController : Controller
    {
        // GET: WAP
        public ActionResult Active()
        {
            return View();
        }
    }
}
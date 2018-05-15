using Orchard.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Enjoy.Core.Controllers
{
    [Themed]
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Summary()
        {
            return View();
        }
    }
}
using Orchard.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Enjoy.Core.Controllers
{
    [Themed]
    public class MarketingController : Controller
    {
        // GET: Marketing
        [Themed]
        public ActionResult Index()
        {
            return View();
        }
    }
}
using Orchard.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Enjoy.Core.Controllers
{
    [Themed]
    public class MerchantController : Controller
    {
        // GET: Default
        [Themed]
        public ActionResult Index()
        {
            return View();
        }
    }
}
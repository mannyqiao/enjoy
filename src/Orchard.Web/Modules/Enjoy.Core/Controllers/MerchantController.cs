using Orchard.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Enjoy.Core.ViewModels;
using Orchard.Mvc.Extensions;

namespace Enjoy.Core.Controllers
{
    [Themed]
    public class MerchantController : Controller
    {
        // GET: Default

        
        public ActionResult Create()
        {
            return View(new MerchantProfileViewModel());
        }
        [ActionName("Create")]
        [HttpPost]
        public ActionResult CreatePost()
        {
            return this.RedirectLocal("/dashboard/summary");
        }
    }
}
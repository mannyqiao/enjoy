using Enjoy.Core.ViewModels;
using Orchard;
using Orchard.Mvc.Extensions;
using Orchard.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Enjoy.Core.Controllers
{
    [Themed]
    public class AccessController : Controller
    {
        private readonly IOrchardServices OS;
        public AccessController(IOrchardServices os)
        {
            this.OS = os;
        }
        // GET: Sign
        public ActionResult Sign()
        {
            var model = new SignViewModel() { Mobile = "13961576298" };
            return View(model);
        }
        [HttpPost]
        public ActionResult Signin(SignViewModel model,string ReturnUrl)
        {
            return this.RedirectLocal("/dashboard/summary");
        }
        [HttpPost]
        public string GetverificationCode(string mobile)
        {
            //// security policy 
            return string.Empty;
        }
        [HttpPost]
        public ActionResult SignUp(SignViewModel model, string ReturnUrl)
        {

            return this.RedirectLocal("/dashboard/summary");
        }
    }
}
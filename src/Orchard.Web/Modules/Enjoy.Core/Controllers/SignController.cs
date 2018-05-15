using Orchard.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Enjoy.Core.Controllers
{
    [Themed]
    public class SignController : Controller
    {
        // GET: Sign
        public ActionResult Signin()
        {
            return View();
        }
        public ActionResult SignOut()
        {
            return View();
        }
        public ActionResult SignUp()
        {
            return View();
        }
    }
}
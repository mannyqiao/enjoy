using Orchard.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Enjoy.Core.Controllers
{
    [Themed]
    public class FinanceController : Controller
    {            

        /// <summary>
        /// 商户账户
        /// </summary>
        /// <returns></returns>
        public ActionResult MyAccount()
        {
            return View();
        }
        /// <summary>
        /// 平台账户
        /// </summary>
        /// <returns></returns>
        public ActionResult PAccount()
        {
            return View();
        }
    }
}
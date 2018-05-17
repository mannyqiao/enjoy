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
        private readonly IWeChatApi WeChat;
        public DashboardController(IWeChatApi api)
        {
            this.WeChat = api;
        }
        // GET: Dashboard
        public ActionResult Summary()
        {
            //"wx0c644f8027d78c74", "f1681068dfcd75ef2d7dff14cb3b5fae"
            
            return View();
        }
    }
}
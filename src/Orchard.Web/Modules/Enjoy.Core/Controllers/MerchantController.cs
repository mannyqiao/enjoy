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
        private readonly IWeChatApi WeChat;
        // GET: Default
        public MerchantController(IWeChatApi api)
        {
            this.WeChat = api;
        }

        public ActionResult Create()
        {
            var model = new CreatingSubMerchantViewModel()
            {
                ApplyProtocol = this.WeChat.GetApplyProtocol()
            };
            return View(model);
        }
        [ActionName("Create")]
        [HttpPost]
        public ActionResult CreatePost()
        {
            return this.RedirectLocal("/dashboard/summary");
        }

        public JsonResult GetApplyProtocol()
        {
            return Json(this.WeChat.GetApplyProtocol(), JsonRequestBehavior.AllowGet);
        }
    }
}
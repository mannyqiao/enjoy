


namespace Enjoy.Core.Controllers
{
    using System.Web.Mvc;
    using Enjoy.Core.ViewModels;
    using Orchard.Mvc.Extensions;
    using Orchard.Themes;
    using System.Collections.Generic;
    using System.Linq;
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
            return Json(this.WeChat.GetApplyProtocol()
                .Categories.Select((ctx) =>
                {
                    return new SelectNodeViewModel()
                    {
                        Id = ctx.PrimaryCategoryId.ToString(),
                        Text = ctx.CategoryName,
                        Items = ctx.SecondaryCategories.Select((child) =>
                        {
                            return new SelectNodeViewModel()
                            {
                                Id = child.SecondaryCategoryId.ToString(),
                                Text = child.CategoryName,
                                Items = new SelectNodeViewModel[] { }
                            };
                        }).ToArray()

                    };
                })
                , JsonRequestBehavior.AllowGet);
        }
    }
}
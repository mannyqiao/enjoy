


namespace Enjoy.Core.Controllers
{
    using System.Web.Mvc;
    using Enjoy.Core.ViewModels;
    using Orchard.Mvc.Extensions;
    using Orchard.Themes;
    using System.Collections.Generic;
    using System.Linq;
    using Enjoy.Core.Services;
    using System.Web;
    using System.IO;
    using Orchard;
    using Enjoy.Core.Models.Records;
    using System;
    [Themed]
    public class MerchantController : Controller
    {
        private readonly IWeChatApi WeChat;
        private readonly IOrchardServices OrchardServices;
        // GET: Default
        public MerchantController(IWeChatApi api, IOrchardServices os)
        {
            this.WeChat = api;
            this.OrchardServices = os;
        }

        public ActionResult Create()
        {
            var model = new CreatingSubMerchantViewModel()
            {
                ApplyProtocol = this.WeChat.GetApplyProtocol(),
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult CreatePost(CreatingSubMerchantViewModel model)
        {
            var record = new Merchant()
            {
                AgreementMediaId = model.AgreementMediaId,
                AppId = string.Empty,
                BenginTime = DateTime.Now.ToUnixStampDateTime(),
                BrandName = model.BrandName,
                Contact = model.Contact,
                CreateTime = DateTime.Now.ToUnixStampDateTime(),
                EndTime = DateTime.Now.AddYears(1).ToUnixStampDateTime(),                
                LogoUrl = model.LogoUrl,
                Mobile = model.Mobile,
                OperatorMediaId = model.OperatorMediaId,
                PrimaryCategoryId = model.PrimaryCategoryId,
                Protocol = model.Protocol,
                SecondaryCategoryId = model.SecondaryCategoryId,
                Status = MerchantStatus.CHECKING.ToString(),
                UpdateTime = DateTime.Now.ToUnixStampDateTime()
            };
            this.OrchardServices.TransactionManager.GetSession().SaveOrUpdate(record);
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
        [HttpPost]
        public JsonResult UploadMaterial()
        {
            var context = this.OrchardServices.WorkContext.HttpContext.Request.Files ?? null;
            if (context == null)
            {
                return Json(new { result = "fail" });
            }

            using (var stream = new BinaryReader(context[0].InputStream))
            {
                var buffers = stream.ReadBytes(context[0].ContentLength);
                var result = this.WeChat.UploadMaterial(context[0].FileName, buffers);
                return Json(result);
            }
        }
    }
}
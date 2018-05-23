


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
        private readonly IOrchardServices OS;
        private readonly IEnjoyAuthService Auth;
        // GET: Default
        public MerchantController(IWeChatApi api, IOrchardServices os, IEnjoyAuthService auth)
        {
            this.WeChat = api;
            this.OS = os;
            this.Auth = auth;
        }

        public ActionResult Create()
        {
            var user = this.Auth.GetAuthenticatedUser();
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
                LogoUrl = string.Empty,// model.LogoUrl,
                Mobile = model.Mobile,
                OperatorMediaId = model.OperatorMediaId,
                PrimaryCategoryId = model.PrimaryCategoryId,
                Protocol = model.Protocol,
                SecondaryCategoryId = model.SecondaryCategoryId,
                Status = MerchantStatus.CHECKING.ToString(),
                UpdateTime = DateTime.Now.ToUnixStampDateTime(),
                EnjoyUser = this.Auth.GetAuthenticatedUser(),
                Address = string.Format("{0}/{1}/{2}", model.Province, model.City, model.Area)

            };
            this.OS.TransactionManager.GetSession().SaveOrUpdate(record);
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
            var context = this.OS.WorkContext.HttpContext.Request.Files ?? null;
            if (context == null)
            {
                return Json(new { result = "fail" }, JsonRequestBehavior.AllowGet);
            }

            using (var stream = new BinaryReader(context[0].InputStream))
            {
                var buffers = stream.ReadBytes(context[0].ContentLength);
                var result = this.WeChat.UploadMaterial(context[0].FileName, buffers);
                return Json(result,JsonRequestBehavior.AllowGet);
            }
        }
    }
}
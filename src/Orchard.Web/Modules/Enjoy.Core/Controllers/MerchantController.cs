


namespace Enjoy.Core.Controllers
{
    using System.Web.Mvc;
    using Enjoy.Core.ViewModels;
    using Orchard.Mvc.Extensions;
    using Orchard.Themes;
    using System.Linq;
    using System.IO;
    using Orchard;

    [Themed]
    public class MerchantController : Controller
    {
        private readonly IWeChatApi WeChat;
        private readonly IOrchardServices OS;
        private readonly IEnjoyAuthService Auth;
        private readonly IMerchantService Merchant;
        private readonly ModelClient client = new ModelClient();
        // GET: Default
        public MerchantController(
            IWeChatApi api,
            IOrchardServices os,
            IEnjoyAuthService auth,
            IMerchantService merchant)
        {
            this.WeChat = api;
            this.OS = os;
            this.Auth = auth;
            this.Merchant = merchant;
        }

        public ActionResult Create()
        {
            var user = this.Auth.GetAuthenticatedUser();

            var model = new CreatingSubMerchantViewModel()
            {
                ApplyProtocol = this.WeChat.GetApplyProtocol(),
                Status = MerchantStatus.NotFond,
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult CreatePost(CreatingSubMerchantViewModel model)
        {
            this.Merchant.CreateSubMerchant(model);
            //Create sub merchant
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
        public JsonResult UploadMaterial(MediaUploadTypes type)
        {
            var context = this.OS.WorkContext.HttpContext.Request.Files ?? null;
            if (context == null)
            {
                return Json(new { result = "fail" }, JsonRequestBehavior.AllowGet);
            }

            using (var stream = new BinaryReader(context[0].InputStream))
            {
                var buffers = stream.ReadBytes(context[0].ContentLength);
                switch (type)
                {
                    case MediaUploadTypes.AuthMaterial:
                        {
                            var result = this.WeChat.UploadMaterial(context[0].FileName, buffers);
                            return Json(result, JsonRequestBehavior.AllowGet);
                        }
                    default:
                        {
                            var result = this.WeChat.UploadMaterialToCDN(buffers);
                            return Json(result, JsonRequestBehavior.AllowGet);
                        }
                }

            }
        }
        [HttpPost]
        public JsonResult UploadMaterialToCDN()
        {
            var context = this.OS.WorkContext.HttpContext.Request.Files ?? null;
            if (context == null)
            {
                return Json(new { result = "fail" }, JsonRequestBehavior.AllowGet);
            }

            using (var stream = new BinaryReader(context[0].InputStream))
            {
                var buffers = stream.ReadBytes(context[0].ContentLength);
                var result = this.WeChat.UploadMaterialToCDN(buffers);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Shops()
        {

        }
    }
}
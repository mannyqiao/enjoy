


namespace Enjoy.Core.Controllers
{
    using System.Web.Mvc;
    using Enjoy.Core.ViewModels;
    using Orchard.Mvc.Extensions;
    using Orchard.Themes;
    using System.Linq;
    using System.IO;
    using Orchard;
    using System.Collections.Generic;
    using System;

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

        public ActionResult MyMerchant(int page = 1)
        {
            if (this.Auth.GetAuthenticatedUser() == null)
                return this.RedirectLocal("/access/sign");

            var viewModel = this.Merchant.QueryMyMerchants(this.Auth.GetAuthenticatedUser().Id, page);
            return View(viewModel);
        }
        public ActionResult Create()
        {
            if (this.Auth.GetAuthenticatedUser() == null)
                return this.RedirectLocal("/access/sign");
            var viewModel = client.Convert(this.Merchant.GetDefaultMerchant(), this.WeChat.GetApplyProtocol());
            return View(viewModel);
        }
        public ActionResult View(int id)
        {
            if (this.Auth.GetAuthenticatedUser() == null)
                return this.RedirectLocal("/access/sign");

            var viewModel = client.Convert(this.Merchant.GetDefaultMerchant(), this.WeChat.GetApplyProtocol());
            return View(this.Merchant.GetDefaultMerchant());
        }
        [HttpPost]
        public ActionResult CreatePost(MerchantViewModel model)
        {
            if (this.Auth.GetAuthenticatedUser() == null)
                return this.RedirectLocal("/access/sign");


            model.Merchant.EnjoyUser = this.Auth.GetAuthenticatedUser();
            model.Merchant.Address = string.Join("/", new string[] { model.Province, model.City, model.Area });
            model.Merchant.Status = AuditStatus.UnCommitted;
            this.Merchant.SaveOrUpdate(model.Merchant);
            return this.RedirectLocal("/merchant/mymerchant?datetime=" + DateTime.Now.ToUnixStampDateTime());
        }
        [HttpPost]
        public ActionResult Audit(MerchantViewModel model)
        {
            if (this.Auth.GetAuthenticatedUser() == null)
                return this.RedirectLocal("/access/sign");
            ////Enjoy TODO: Need add code that audit by wechat.
            return this.RedirectLocal("/merchant/mymerchant?datetime=" + DateTime.Now.ToUnixStampDateTime());

        }

        public JsonResult GetApplyProtocol()
        {
            return Json(client.Convert(this.WeChat.GetApplyProtocol()), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UploadMaterial(MediaUploadTypes type)
        {
            var context = this.OS.WorkContext.HttpContext.Request.Files ?? null;
            if (context == null || context.Count.Equals(0))
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
                            ////TODO 需要返回 url
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
        public ActionResult MyShops()
        {
            var viewModel = new ShopListViewModel()
            {
                Items = new List<ShopViewModel>()
                {
                    new ShopViewModel(){
                         Id =1,
                         Address = "眉山市东坡区东坡里步行街",
                         Coordinate  ="{lat:1.032,lng:3033 }",
                         Leader = "张三",
                         Merchant =  new Models.Records.Merchant(){  BrandName="柠檬工坊-01"},
                         ShopName = "柠檬工坊东坡店"
                    },
                    new ShopViewModel(){
                         Id =1,
                         Address = "眉山市东坡区东坡里步行街",
                         Coordinate  ="{lat:1.032,lng:3033 }",
                         Leader = "张三",
                         Merchant =  new Models.Records.Merchant(){  BrandName="柠檬工坊-01"},
                         ShopName = "柠檬工坊东坡店"
                    }
                },
                Paging = new PagingCondition(1, 20)
            };
            return View(viewModel);
        }

    }
}
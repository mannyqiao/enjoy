


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
    using Enjoy.Core.Models;
    using Enjoy.Core;
    [Themed]
    public class MerchantController : Controller
    {
        private readonly IWeChatApi WeChat;
        private readonly IOrchardServices OS;
        private readonly IEnjoyAuthService Auth;
        private readonly IMerchantService Merchant;
        private readonly IShopService Shop;
        private readonly ModelClient client = new ModelClient();
        // GET: Default
        public MerchantController(
            IWeChatApi api,
            IOrchardServices os,
            IEnjoyAuthService auth,
            IShopService shop,
            IMerchantService merchant)
        {
            this.WeChat = api;
            this.OS = os;
            this.Auth = auth;
            this.Merchant = merchant;
            this.Shop = shop;
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
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult CreatePost(MerchantViewModel model)
        {
            if (this.Auth.GetAuthenticatedUser() == null)
                return this.RedirectLocal("/access/sign");
            model.Merchant.EnjoyUser = this.Auth.GetAuthenticatedUser();
            model.Merchant.Address = string.Join("/", new string[] { model.Province, model.City, model.Area });
            model.Merchant.BeginTime = model.StartTimeString.ToDateTime().ToUnixStampDateTime();
            model.Merchant.EndTime = model.EndTimeString.ToDateTime().ToUnixStampDateTime();
            model.Merchant.Status = AuditStatus.UnCommitted;
            this.Merchant.SaveOrUpdate(model.Merchant);
            return this.RedirectLocal("/merchant/mymerchant?datetime=" + DateTime.Now.ToUnixStampDateTime());
        }
        [HttpPost]
        public JsonNetResult Audit(int id)
        {
            var model = this.Merchant.GetDefaultMerchant(id);
            var data = this.Merchant.SaveAndPushToWeChat(model);
            return new JsonNetResult() { Data = data };
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
            if (this.Auth.GetAuthenticatedUser() == null)
                return this.RedirectLocal("/access/sign");
            var merchant = this.Merchant.GetDefaultMerchant();
            if (merchant == null)
                return this.RedirectLocal("/merchant/create");
            return View();
        }
        [HttpPost]
        public JsonNetResult QueryMyShops(QueryFilter filter)
        {
            if (this.Auth.GetAuthenticatedUser() == null)
                return new JsonNetResult() { Data = new { } };
            var merchant = this.Merchant.GetDefaultMerchant();
            filter.Columns.Add(new QueryColumnFilter()
            {
                Data = "Merchant.Id",
                Searchable = true,
                Search = new SearchColumnFilter() { Regex = false, Value = merchant.Id }
            });

            var condition = new PagingCondition(filter.Start, filter.Length);
            var model = client.Convert(this.Shop.QueryShops(filter, condition));
            model.Draw = filter.Draw;
            return new JsonNetResult() { Data = model };
        }

        public ActionResult EditShop(int? id = null)
        {
            if (this.Auth.GetAuthenticatedUser() == null)
                return this.RedirectLocal("/access/sign");
            var merchant = this.Merchant.GetDefaultMerchant();

            var viewModel = id == null
                ? new ShopViewModel(new Models.ShopModel(merchant))
                : new ShopViewModel(this.Shop.GetDefaultShop(id.Value));
            viewModel.Protocol = this.WeChat.GetApplyProtocol();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditShopPost(ShopViewModel viewModel, string returnUrl)
        {
            //var merchant = this.Merchant.GetDefaultMerchant();
            var model = new Models.ShopModel(viewModel);
            this.Shop.SaveOrUpdate(model);
            return this.RedirectLocal(returnUrl);
        }
        [HttpPost]
        public JsonNetResult Delete(int? id = null, string returnUrl = "/merchant/myshops")
        {
            if (id == null) return new JsonNetResult() { Data = new { result = "fail" } };
            this.Shop.DeleteShop(id.Value);
            return new JsonNetResult()
            {
                Data = new BaseResponse(EnjoyConstant.Success)
            };
        }
    }
}
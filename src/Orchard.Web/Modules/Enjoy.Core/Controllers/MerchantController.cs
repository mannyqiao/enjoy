


namespace Enjoy.Core.Controllers
{
    using System.Web.Mvc;
    using Enjoy.Core.ViewModels;
    using Orchard.Mvc.Extensions;
    using Orchard.Themes;
    using System.IO;
    using Orchard;
    using System;
    using Enjoy.Core.EnjoyModels;
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
                return this.RedirectLocal("/access/sign?signin=true");

            var cc = this.WeChat.QueryCardCouponOnWechat();
            if (cc.HasError == false)
            {
                foreach (var card in cc.CardIdList)
                {
                    var result = this.WeChat.DeleteCardCoupon(card);
                }
            }
            

            return View();

        }
        public ActionResult Create()
        {
            if (this.Auth.GetAuthenticatedUser() == null)
                return this.RedirectLocal("/access/sign?signin=true");
            var model = new MerchantModel()
            {
                EnjoyUser = new EnjoyUserModel() { Id = this.Auth.GetAuthenticatedUser().Id }
            };
            var viewModel = client.Convert(model, this.WeChat.GetApplyProtocol());
            return View(viewModel);
        }
        public ActionResult View(long id)
        {
            if (this.Auth.GetAuthenticatedUser() == null)
                return this.RedirectLocal("/access/sign?signin=true");

            var viewModel = client.Convert(this.Merchant.GetDefaultMerchant(id), this.WeChat.GetApplyProtocol());
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult CreatePost(MerchantViewModel model)
        {
            //if (this.OS.WorkContext.GetState<IEnjoyUser>(EnjoyConstant.EnjoyCurrentUser) == null)
            //    return this.RedirectLocal("/access/sign");

            model.Merchant.EnjoyUser = this.Auth.GetAuthenticatedUser() as EnjoyUserModel;
            model.Merchant.Address = string.Join("/", new string[] { model.Province, model.City, model.Area });
            model.Merchant.BeginTime = DateTime.UtcNow.ToUnixStampDateTime(); //model.StartTimeString.ToDateTime().ToUnixStampDateTime();
            model.Merchant.EndTime = model.EndTimeString.ToDateTime().ToUnixStampDateTime();
            model.Merchant.Status = AuditStatus.UnCommitted;
            if (model.Merchant.Id.Equals(0))
                model.Merchant.CreateTime = DateTime.UtcNow.ToUnixStampDateTime();
            model.Merchant.UpdateTime = DateTime.UtcNow.ToUnixStampDateTime();
            this.Merchant.SaveOrUpdate(model.Merchant);
            return this.RedirectLocal("/merchant/mymerchant?datetime=" + DateTime.UtcNow.ToUnixStampDateTime());
        }
        [HttpPost]
        public JsonNetResult Audit(long id)
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
        public ActionResult MyShops(long? merchantid = null)
        {
            if (this.Auth.GetAuthenticatedUser() == null)
                return this.RedirectLocal("/access/sign?signin=true");
            var merchant = this.Merchant.GetDefaultMerchant(merchantid ?? 0);
            return View(merchant);
        }
        [HttpPost]
        public JsonNetResult QueryMyShops(WebQueryFilter filter)
        {
            if (this.Auth.GetAuthenticatedUser() == null)
                return new JsonNetResult() { Data = new { } };
            var merchant = this.Merchant.GetDefaultMerchant();
            if (filter.Fixation != null)
            {
                foreach (var key in filter.Fixation.Keys)
                {
                    var column = new QueryColumnFilter()
                    {
                        Data = key,
                        Searchable = true,
                        Search = new SearchColumnFilter() { Regex = false, Value = filter.Fixation[key] }
                    };
                    if (key.Equals("Merchant.Id", StringComparison.OrdinalIgnoreCase))
                    {
                        column.DbType = System.Data.DbType.Int64;
                    }
                    filter.Columns.Add(column);
                }
            }

            var condition = new PagingCondition(filter.Start, filter.Length);
            var model = client.Convert(this.Shop.QueryShops(filter, condition));
            model.Draw = filter.Draw;
            return new JsonNetResult() { Data = model };
        }
        [HttpPost]
        public JsonNetResult QueryMyMerchant(WebQueryFilter filter)
        {
            if (this.Auth.GetAuthenticatedUser() == null)
                return new JsonNetResult() { Data = new { } };

            filter.Columns.Add(new QueryColumnFilter()
            {
                Data = "EnjoyUser.Id",
                Searchable = true,
                DbType = System.Data.DbType.Int64,
                Search = new SearchColumnFilter() { Regex = false, Value = this.Auth.GetAuthenticatedUser().Id }
            });

            var condition = new PagingCondition(filter.Start, filter.Length);
            var viewModel = this.Merchant.QueryMerchants(filter, condition);
            return new JsonNetResult() { Data = viewModel };
        }
        public JsonNetResult Del(long id)
        {
            if (this.Auth.GetAuthenticatedUser() == null)
                return new JsonNetResult() { Data = new { } };
            var merchant = this.Merchant.GetDefaultMerchant(id);
            if (merchant.Status == AuditStatus.UnCommitted || merchant.Status == AuditStatus.REJECTED)
            {
                return new JsonNetResult() { Data = this.Merchant.Delete(id) };
            }
            else
            {
                return new JsonNetResult()
                {
                    Data = new BaseResponse(Constants.CanNotDeleteMerchant)
                };
            }
        }
        public ActionResult EditShop(long merchantid, int? id = null)
        {
            if (this.Auth.GetAuthenticatedUser() == null)
                return this.RedirectLocal("/access/sign");
            var merchant = this.Merchant.GetDefaultMerchant(merchantid);

            var viewModel = id == null
                ? new ShopViewModel(new ShopModel(merchant))
                : new ShopViewModel(this.Shop.GetDefaultShop(id.Value));
            viewModel.Protocol = this.WeChat.GetApplyProtocol();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditShopPost(ShopViewModel viewModel, string returnUrl)
        {
            var model = new ShopModel(viewModel);
            this.Shop.SaveOrUpdate(model);
            return this.RedirectLocal(string.Format("/merchat/myshops?merchantid={0}", model.Merchant.Id));
        }
        [HttpPost]
        public JsonNetResult DelShop(int? id = null, string returnUrl = "/merchant/myshops")
        {

            if (id == null) return new JsonNetResult() { Data = new { result = "fail" } };
            this.Shop.DeleteShop(id.Value);
            return new JsonNetResult()
            {
                Data = new BaseResponse(Constants.Success)
            };
        }
    }
}
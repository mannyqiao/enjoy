

namespace Enjoy.Core.Controllers
{
    using Orchard.Themes;
    using System;
    using System.Web.Mvc;
    using Orchard.Mvc.Extensions;
    using Enjoy.Core.EnjoyModels;

    using Orchard;
    using System.Linq;
    using Enjoy.Core.ViewModels;

    [Themed]
    public class CardsController : Controller
    {
        private readonly IOrchardServices OS;
        private readonly IWeChatApi WeChat;
        private readonly ICardCouponService CardCoupon;
        private readonly IMerchantService Merchant;
        private readonly IEnjoyAuthService Auth;
        private ModelClient client = new ModelClient();
        public CardsController(
            IOrchardServices os,
            ICardCouponService cardcoupn,
            IMerchantService merchant,
            IEnjoyAuthService auth,

            IWeChatApi wechat)
        {
            this.OS = os;
            this.WeChat = wechat;
            this.CardCoupon = cardcoupn;
            this.Merchant = merchant;
            this.Auth = auth;
        }
        // GET: Cards
        /// <summary>
        /// 优惠券a
        /// </summary>
        /// <returns></returns>

        public ActionResult Coupon(long? merchantid)
        {
            if (this.Auth.GetAuthenticatedUser() == null)
                return this.RedirectLocal("/access/sign?signin=true");
            var merchant = merchantid == null
                ? this.Merchant.GetDefaultMerchant()
                : this.Merchant.GetDefaultMerchant(merchantid.Value);

            if (merchant == null || merchant.Id.Equals(0))
                return this.RedirectLocal("/merchant/create");

            return View(new MerchantCardCouponViewModel(merchant)
            {
                CardType = CardTypes.DISCOUNT
            });
        }
        [HttpPost]
        public JsonNetResult QueryCouponCard(QueryFilter filter)
        {

            if (this.Auth.GetAuthenticatedUser() == null)
                return new JsonNetResult() { Data = new { } };

            var merchant = this.Merchant.GetDefaultMerchant();
            filter.Columns.Add(new QueryColumnFilter()
            {
                Data = "Merchant.Id",
                Searchable = true,
                DbType = System.Data.DbType.Int64,
                Search = new SearchColumnFilter() { Regex = false, Value = merchant.Id }
            });
            var model = this.CardCoupon.QueryCardCoupon(filter, new PagingCondition(filter.Start, filter.Length));
            var viewModel = new PagingData<CardCouponWithoutWapperViewModel>(model.Items.Select(o => new CardCouponWithoutWapperViewModel(o)))
            {
                Paging = model.Paging,
                TotalCount = model.TotalCount
            };
            return new JsonNetResult() { Data = viewModel };
        }
        /// <summary>
        /// 创建优惠券
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(long merchantid, long? id = null, CardTypes type = CardTypes.DISCOUNT)
        {
            //var xx = this.WeChat.DeleteCardCoupon("p8ntH0ly5Wtdauv5pLm4NuX1W8jI");
            //xx = this.WeChat.DeleteCardCoupon("p8ntH0uMYrVs9q1jcw4W2iuUIbmg");
            //xx = this.WeChat.DeleteCardCoupon("p8ntH0nfTZdBv8NwTY2afZUoojD8");

            if (this.Auth.GetAuthenticatedUser() == null)
                return this.RedirectLocal("/access/sign?signin=true");

            var merchant = this.Merchant.GetDefaultMerchant(merchantid);
            //如果商户没有创建则返回到商户管理界面，
            if (merchant == null) this.RedirectLocal("/merchant/mymerchant");
            var viewModel = id == null
                ? type.Initialize(merchant).Convert()
                : this.CardCoupon.GetCardCounpon(id.Value).Convert();
            viewModel.MerchantId = merchant.Id;
            
            viewModel.SubMerchantBrandName = merchant.BrandName;
            return View(type.GetViewName(), viewModel);
        }
        [HttpPost]
        public ActionResult EditPost(CardCounponViewModel viewModel, string ReturnUrl)
        {
            if (this.Auth.GetAuthenticatedUser() == null)
                return this.RedirectLocal("/access/sign?signin=true");
            var merchant = this.Merchant.GetDefaultMerchant(viewModel.MerchantId);
            viewModel.State = CardCouponStates.Editing;
            var result = this.CardCoupon.SaveOrUpdate(viewModel.Convert(merchant));
            return this.RedirectLocal("/cards/coupon?datetime=" + DateTime.Now.ToUnixStampDateTime());
        }
        [HttpPost]
        public JsonNetResult Publish(long id)
        {
            var result = this.CardCoupon.Publish(id);
            return new JsonNetResult() { Data = result };
        }
        [HttpPost]
        public JsonNetResult Delete(long id)
        {
            return new JsonNetResult()
            {
                Data = this.CardCoupon.DeleteById(id)
            };
        }
        public ActionResult ShowQR(int id)
        {
            var model = this.CardCoupon.GetCardCounpon(id);
            var qrcode = this.CardCoupon.CreateQRCode(model.WxNo);
            var viewModel = new QRCodeViewModel() { QRCodeUrl = qrcode.ShowQRCodeUrl };
            return View(viewModel);
        }

     
        [HttpPost]
        public ActionResult EditMemberCard(CardCounponViewModel viewModel, string ReturnUrl)
        {
            if (this.Auth.GetAuthenticatedUser() == null)
                return this.RedirectLocal("/access/sign?signin=true");
            //viewModel.MerberCard.ActivateAppBrandPass = "pages/store/index";
            //viewModel.MerberCard.ActivateAppBrandUserName = "gh_e1543e2be86d@app";
            //viewModel.MerberCard.ActivateUrl = "https://www.yourc.com/m/active";            

            viewModel.State = CardCouponStates.Editing;
            var merchant = this.Merchant.GetDefaultMerchant(viewModel.MerchantId);            
            var result = this.CardCoupon.SaveOrUpdate(viewModel.WithFixedSettings().Convert(merchant));          
            
            return this.RedirectLocal("/cards/coupon?datetime=" + DateTime.Now.ToUnixStampDateTime());
        }
        public ActionResult Query(int id)
        {
            var model = this.CardCoupon.GetCardCounpon(id);
            return this.RedirectLocal("/cards/coupon?datetime=" + DateTime.Now.ToUnixStampDateTime());
        }
    }
}


namespace Enjoy.Core.Controllers
{
    using Orchard.Themes;
    using System;
    using System.Web.Mvc;
    using Orchard.Mvc.Extensions;
    using Enjoy.Core.ViewModels;

    using Orchard;
    using System.Linq;
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

        public ActionResult Coupon()
        {
            if (this.Auth.GetAuthenticatedUser() == null)
                return this.RedirectLocal("/access/sign");
            return View();
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
                Search = new SearchColumnFilter() { Regex = false, Value = merchant.Id }
            });
            var model = this.CardCoupon.QueryCardCoupon(filter, new PagingCondition(filter.Start, filter.Length));
            var viewModel = new Models.PagingData<CardCouponWithoutWapperViewModel>(model.Items.Select(o => new CardCouponWithoutWapperViewModel(o)))
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
        public ActionResult Edit(int? id = null, CardTypes type = CardTypes.DISCOUNT)
        {
            var viewModel = id == null
                ? new CardCounponViewModel() { CardType = CardTypes.DISCOUNT, CCStatus = CCStatus.Editing }
                : client.Convert(this.CardCoupon.GetCardCounpon(id.Value));
            switch (type)
            {
                case CardTypes.MEMBER_CARD:
                    return View("EditMCard", viewModel);
                default:
                    return View("EditCoupon", viewModel);
            }
        }
        [HttpPost]
        public ActionResult EditPost(CardCounponViewModel viewModel, string ReturnUrl)
        {
            if (this.Auth.GetAuthenticatedUser() == null)
                return this.RedirectLocal("/access/sign");

            var result = this.CardCoupon.SaveOrUpdate(client.Convert(viewModel, this.Merchant.GetDefaultMerchant()));
            return this.RedirectLocal("/cards/coupon?datetime=" + DateTime.Now.ToUnixStampDateTime());
        }
        [HttpPost]
        public JsonNetResult Publish(int id)
        {
            var result = this.CardCoupon.Publish(id);
            return new JsonNetResult() { Data = result };
        }
        public ActionResult ShowQR(int id)
        {
            var model = this.CardCoupon.GetCardCounpon(id);
            var qrcode = this.CardCoupon.CreateQRCode(model.WxNo);
            var viewModel = new QRCodeViewModel() { QRCodeUrl = qrcode.ShowQRCodeUrl };
            return View(viewModel);
        }

        /// <summary>
        /// 创建会员卡
        /// </summary>
        /// <returns></returns>
        //public ActionResult CreateMCard()
        //{
        //    var viewModel = new CardCounponViewModel()
        //    {
        //        CardType = CardTypes.MEMBER_CARD,
        //        CCStatus = CCStatus.Editing
        //    };
        //    return View(viewModel);
        //}

        public ActionResult Query(int id)
        {
            var model = this.CardCoupon.GetCardCounpon(id);
            return this.RedirectLocal("/cards/coupon?datetime=" + DateTime.Now.ToUnixStampDateTime());
        }
    }
}
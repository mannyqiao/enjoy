﻿

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
        public ActionResult Coupon(int page = 1, CardTypes type = CardTypes.None)
        {
            if (this.Auth.GetAuthenticatedUser() == null)
                return this.RedirectLocal("/access/sign");

            var result = this.CardCoupon.QueryCardCounpon(page, CardTypes.None);
            var viewModel = new Models.PagingData<CardCouponWithoutWapperViewModel>(result.Items.Select(o => new CardCouponWithoutWapperViewModel(o)))
            {
                Paging = result.Paging,
                TotalCount = result.TotalCount
            };

            return View(viewModel);
        }

        /// <summary>
        /// 会员卡
        /// </summary>
        /// <returns></returns>
        public ActionResult MCard()
        {
            return View();
        }
        /// <summary>
        /// 创建优惠券
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateCoupon()
        {
            var viewModel = new CardCounponViewModel();
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult CreateCouponPost(CardCounponViewModel viewModel, string ReturnUrl)
        {
            if (this.Auth.GetAuthenticatedUser() == null)
                return this.RedirectLocal("/access/sign");
            var result = this.CardCoupon.SaveOrUpdate(client.Convert(viewModel, this.Merchant.GetDefaultMerchant()));
            return this.RedirectLocal("/cards/coupon?datetime=" + DateTime.Now.ToUnixStampDateTime());
        }
        public ActionResult Publish(int id)
        {
            var result = this.CardCoupon.Publish(id);
         
            return this.RedirectLocal("/cards/coupon?datetime=" + DateTime.Now.ToUnixStampDateTime());
        }
        public ActionResult ShowQR(int id)
        {
            var model = this.CardCoupon.GetCardCounpon(id);
            var qrcode = this.CardCoupon.CreateQRCode(model.WxNo);
            var viewModel = new QRCodeViewModel() { QRCodeUrl = qrcode.ShowQRCodeUrl };
            return View(viewModel);
            //return this.RedirectLocal("/cards/coupon?datetime=" + DateTime.Now.ToUnixStampDateTime());
        }
        /// <summary>
        /// 创建会员卡
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateMCard()
        {
            return View();

        }

    }
}
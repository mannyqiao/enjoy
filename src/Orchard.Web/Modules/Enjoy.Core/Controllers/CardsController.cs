using Orchard.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Enjoy.Core.UIElements;
using Orchard.Mvc.Extensions;
using Enjoy.Core.ViewModels;
using Orchard;
using System.IO;

namespace Enjoy.Core.Controllers
{
    [Themed]
    public class CardsController : Controller
    {
        private readonly IOrchardServices OS;
        private readonly IWeChatApi WeChat;
        public CardsController(
            IOrchardServices os,
            IWeChatApi wechat)
        {
            this.OS = os;
            this.WeChat = wechat;
        }
        // GET: Cards
        /// <summary>
        /// 优惠券a
        /// </summary>
        /// <returns></returns>
        public ActionResult DCoupon()
        {
            return View();
        }
        /// <summary>
        /// 团购券
        /// </summary>
        /// <returns></returns>
        public ActionResult GCoupon()
        {
            return View();
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
        public ActionResult CreateDCoupon()
        {
            var viewModel = new CardCounponViewModel();
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult CreateDCouponPost(CardCounponViewModel segment, string ReturnUrl)
        {
            //var metaData = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(SomeViewModel));
            
            return this.RedirectLocal(ReturnUrl);
        }
        /// <summary>
        /// 创建团购券
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateGConpon()
        {

            return View();
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
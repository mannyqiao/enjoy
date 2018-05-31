using Orchard.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Enjoy.Core.Controllers
{
    [Themed]
    public class CardsController : Controller
    {
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
            return View();
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
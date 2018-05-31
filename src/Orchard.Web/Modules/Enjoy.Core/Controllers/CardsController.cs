using Orchard.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Enjoy.Core.UIElements;


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
            var viewModel = new TabContainerSegment("tabContainer", "新建券")
            {
                TabNavs = new TabNavSegment[] {
                    new TabNavSegment("#baseInfo","基本信息",true),
                    new TabNavSegment("#useRule","使用规则",false),
                    new TabNavSegment("#settings","外观设置",false),
                },
                TabPanes = new TabPaneSegment[]
                {
                    new TabPaneSegment("baseInfo",string.Empty,true){
                         Elements = new GroupUIElement[]
                         {
                             new GroupUIElement("group_type","券类型",new UIElement[]{
                                 new RadioUIElement(new RadioUIElement.RadioItem[]{
                                     new RadioUIElement.RadioItem("radio_discount","折扣券",CardTypes.DISCOUNT.ToString(),"group_discount"),
                                     new RadioUIElement.RadioItem("radio_cash","现金券",CardTypes.CASH.ToString(),"group_cash"),
                                     new RadioUIElement.RadioItem("radio_gift","礼品券",CardTypes.GIFT.ToString(),"group_gift")
                                 },
                                 new string[]{"group_discount","group_gift","group_cash" },"radio_types",CardTypes.DISCOUNT.ToString())
                             }),
                             new GroupUIElement("coupon_name","券名称",new UIElement[]{
                                 new TextUIElement("CouponName","券名称",string.Empty,"输入券名称",true)
                             }),
                             new GroupUIElement("discount","折扣额度",new UIElement[]{
                                 new TextUIElement("Discount","折扣额度",string.Empty,"输入折扣额度",true)
                             }),
                             new GroupUIElement("cash","",new UIElement[]{
                                 new TextUIElement("CashMoney","优惠金额",string.Empty,"输入优惠金额",true),
                                 new TextAreaUIElement("CashDescription","优惠详情",string.Empty,"输入优惠详情",true)
                             }),
                             new GroupUIElement("gift","",new UIElement[]{
                                 new TextUIElement("GiftDescription","兑换详情",string.Empty,"输入兑换详情",true)
                             })
                         }
                    }
                    ,
                    new TabPaneSegment("#useRule",string.Empty,false){
                        Elements = new GroupUIElement[]{ }
                    },
                    new TabPaneSegment("#settings",string.Empty,false){
                        Elements = new GroupUIElement[]{ }
                    }

                }
            };
            return View(viewModel);
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
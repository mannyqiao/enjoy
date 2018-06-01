using Orchard.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Enjoy.Core.UIElements;
using Orchard.Mvc.Extensions;
using Enjoy.Core.ViewModels;

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
            var viewModel = new TabContainerSegment<CardCounponViewModel>("tabContainer", "新建券")
            {
                TabNavs = new TabNavSegment[] {
                    new TabNavSegment("baseInfo","基本信息",true),
                    new TabNavSegment("useRule","使用规则",false),
                    new TabNavSegment("settings","外观设置",false),
                },
                TabPanes = new TabPaneSegment<CardCounponViewModel>[]
                {
                    new TabPaneSegment<CardCounponViewModel>("baseInfo",string.Empty,true){
                         Elements = new UIElement<CardCounponViewModel,object>[]
                         {
                             new UIElement<CardCounponViewModel,CardTypes>(UIType.GRadio,"券类型",o=>o.CardType)
                             //new UIElement<CardCounponViewModel>( UIType.GRadio,o=>o.,"券类型",new UIElement[]{
                             //    new RadioUIElement(new RadioUIElement.RadioItem[]{
                             //        new RadioUIElement.RadioItem("radio_discount","折扣券",CardTypes.DISCOUNT.ToString(),"group_discount"),
                             //        new RadioUIElement.RadioItem("radio_cash","现金券",CardTypes.CASH.ToString(),"group_cash"),
                             //        new RadioUIElement.RadioItem("radio_gift","礼品券",CardTypes.GIFT.ToString(),"group_gift")
                             //    },
                             //    new string[]{"group_discount","group_gift","group_cash" },"radio_types",CardTypes.DISCOUNT.ToString())
                             //}),
                             //new GroupUIElement("coupon_name","券名称",new UIElement[]{
                             //    new TextUIElement( ,"券名称",string.Empty,"输入券名称",true)
                             //}),
                             //new GroupUIElement("discount","折扣额度",new UIElement[]{
                             //    new TextUIElement("Discount","折扣额度",string.Empty,"输入折扣额度",true)
                             //}),
                             //new GroupUIElement("cash","",new UIElement[]{
                             //    new TextUIElement("CashMoney","优惠金额",string.Empty,"输入优惠金额",true),
                             //    new TextAreaUIElement("CashDescription","优惠详情",string.Empty,"输入优惠详情",true)
                             //}),
                             //new GroupUIElement("gift","",new UIElement[]{
                             //    new TextAreaUIElement("GiftDescription","兑换详情",string.Empty,"输入兑换详情",true)
                             //}),
                             //new GroupUIElement("sku","",new UIElement[]{
                             //    new TextUIElement("sku","库存量",string.Empty,"输入库存量",true)
                             //}),
                             //new GroupUIElement("quota","",new UIElement[]{
                             //    new TextUIElement("quota","每人限领",string.Empty,"每人限领数理",true)
                             //}),
                         }
                    }
                    ,
                    new TabPaneSegment("useRule",string.Empty,false){
                        Elements = new GroupUIElement[]
                        {
                             new GroupUIElement("group_shop_scope","适用门店",new UIElement[]{
                                 new RadioUIElement(new RadioUIElement.RadioItem[]{
                                     new RadioUIElement.RadioItem("radio_allshop","所有门店",ApplyScopes.All.ToString(),"group_all"),
                                     new RadioUIElement.RadioItem("radio_specific","特定门店",CardTypes.CASH.ToString(),"group_specific")
                                 },
                                 new string[]{ "group_all", "group_specific"},"radio_types",CardTypes.DISCOUNT.ToString())
                             }),
                             new GroupUIElement("group_time_scope","可用时段",new UIElement[]{
                                 new RadioUIElement(new RadioUIElement.RadioItem[]{
                                     new RadioUIElement.RadioItem("radio_alltime","所有时段",ApplyScopes.All.ToString(),"group_all"),
                                     new RadioUIElement.RadioItem("radio_specific","特定时段",ApplyScopes.Specific.ToString(),"group_specific")
                                 },
                                 new string[]{ "group_all", "group_specific"},"radio_types",ApplyScopes.All.ToString())
                             }),
                             new GroupUIElement("group_product_scope","适用商品",new UIElement[]{
                                 new RadioUIElement(new RadioUIElement.RadioItem[]{
                                     new RadioUIElement.RadioItem("radio_allshop","全部商品",ApplyScopes.All.ToString(),"group_all"),
                                     new RadioUIElement.RadioItem("radio_specific","特定商品",ApplyScopes.Specific.ToString(),"group_specific")
                                 },
                                 new string[]{ "group_all", "group_specific"},"radio_types",ApplyScopes.All.ToString())
                             }),
                              new GroupUIElement("use_description","",new UIElement[]{
                                 new TextAreaUIElement("GiftDescription","使用须知",string.Empty,"输入使用须知",false)
                             }),
                        }
                    },
                    new TabPaneSegment("settings",string.Empty,false){
                        Elements = new GroupUIElement[]{ }
                    }

                }
            };

            return View(viewModel);
        }
        [HttpPost]
        public ActionResult CreateDCouponPost(CardCounponViewModel segment, string ReturnUrl)
        {
            var metaData = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(SomeViewModel));

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


namespace Enjoy.Core.ViewModels
{
    using System;
    using WeChat.Models;
    using System.Linq;
    public class CardCounponViewModel
    {
        public CardCounponViewModel()
        {
            this.AdvancedInfo = new AdvancedInfo()
            {
                UseCondition = new UseCondition(),
                Abstract = new Abstract(),
                TextImageList = new System.Collections.Generic.List<TextImage>() { new TextImage(), new TextImage(), new TextImage() },
                BusinessService = new string[] { }
            };
            this.BaseInfo = new BaseInfo()
            {
                Sku = new Sku() { Quantity = 1000 },
                Dateinfo = new DateInfo()
                {
                    BeginTimestamp = DateTime.UtcNow.ToUnixStampDateTime(),
                    EndTimestamp = DateTime.UtcNow.AddMonths(1).ToUnixStampDateTime(),
                },
                Color = EnjoyConstant.CouponBackgroundColors.Values.FirstOrDefault(),
                Getlimit = 1,
                CenterTitle = "立即使用",
                CenterUrl = "http://wwww.baidu.com"//打开商户小程序的Url
            };

            this.Cash = new CashSpecific();
            this.Discount = new DiscountSpecific();
            this.Gift = new GiftSpecific();
            this.General = new GeneralCouponSpecific();
            this.Groupon = new GrounponSpecific();
            this.FixedExpiryDateDescriptor = new string[] { string.Empty, string.Empty };
            this.SpecifiedExpiryDateDescriptor = new string[] { string.Empty, string.Empty };
            this.AdvancedInfo.UseCondition.CanUseWithOtherDiscount = false;
            this.BaseInfo.CanShare = true;
            this.BaseInfo.CanGivefriend = true;

        }
        public int Id { get; set; }
        public CardTypes CardType { get; set; }
        public BaseInfo BaseInfo { get; set; }
        public AdvancedInfo AdvancedInfo { get; set; }
        public CashSpecific Cash { get; set; }
        public DiscountSpecific Discount { get; set; }
        public GiftSpecific Gift { get; set; }
        public GrounponSpecific Groupon { get; set; }
        public GeneralCouponSpecific General { get; set; }
        public ApplyScopes UseProductScope { get; set; }
        public ApplyScopes UseShopScope { get; set; }
        public long CreatedTime { get; set; }
        /// <summary>
        /// 使用限制 (使用条件)
        /// </summary>
        public UseLimitTypes UseLimitType { get; set; }
        public decimal CostMoneyCanUse { get; set; }
        public ExpiryDateTypes ExpiryDateType { get; set; }
        //Fixed = 1,
        //Specified = 2,
        public string[] FixedExpiryDateDescriptor { get; set; }
        public string[] SpecifiedExpiryDateDescriptor { get; set; }
        //public ApplyScopes SpendScope { get; set; }
        public string[] AllowShops { get; set; }
    }
    public class CashSpecific
    {
        public decimal? LeastCost { get; set; }
        public decimal? ReduceCost { get; set; }
    }
    public class DiscountSpecific
    {
        public decimal? Discount { get; set; }
    }
    public class GiftSpecific
    {
        public string Detail { get; set; }
    }
    public class GrounponSpecific
    {
        public decimal? DValue { get; set; }
        public decimal? SaleValue { get; set; }
        public string Detail { get; set; }
    }
    public class GeneralCouponSpecific
    {
        public string DefaultDetail { get; set; }
    }
}
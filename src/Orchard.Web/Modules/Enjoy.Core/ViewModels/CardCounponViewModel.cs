

namespace Enjoy.Core.ViewModels
{
    using WeChat.Models;
    public class CardCounponViewModel
    {
        public CardCounponViewModel()
        {
            this.AdvancedInfo = new AdvancedInfo();
            this.BaseInfo = new BaseInfo();
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

        /// <summary>
        /// 使用限制 (使用条件)
        /// </summary>
        public UseLimitTypes UseLimitType { get; set; }
        public string CostMoneyCanUse { get; set; }
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
        public string DValue { get; set; }
        public string SaleValue { get; set; }
    }
    public class DiscountSpecific
    {
        public string Discount { get; set; }
    }
    public class GiftSpecific
    {
        public string Detail { get; set; }
    }
    public class GrounponSpecific
    {
        public string DValue { get; set; }
        public string SaleValue { get; set; }
        public string Detail { get; set; }
    }
    public class GeneralCouponSpecific
    {
        public string ReduceMoney { get; set; }
    }
}
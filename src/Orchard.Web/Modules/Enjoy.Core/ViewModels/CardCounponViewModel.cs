

namespace Enjoy.Core.ViewModels
{
    using System;

    using System.Linq;
    using Enjoy.Core.EnjoyModels;
    using Enjoy.Core.WeChatModels;

    public class CardCounponViewModel
    {
        public CardCounponViewModel() { }
        public long Id { get; set; }
        public long MerchantId { get; set; }
        public string SubMerchantBrandName { get; set; }
        public string WxNo { get; set; }
        public CardTypes CardType { get; set; }

        //public CardCoupon<ICardCoupon> Wapper { get; set; }
        //public BaseInfo BaseInfo { get; set; }
        //public AdvancedInfo AdvancedInfo { get; set; }
        public CashCoupon Cash { get; set; }
        public DiscountCoupon Discount { get; set; }
        public GiftCoupon Gift { get; set; }
        public Groupon Groupon { get; set; }
        public GeneralCoupon General { get; set; }
        public MemberCard MerberCard { get; set; }

        //public ApplyScopes UseProductScope { get; set; }
        //public ApplyScopes UseShopScope { get; set; }

        public long CreatedTime { get; set; }
        /// <summary>
        /// 使用限制 (使用条件)
        /// </summary>
        //public UseLimitTypes UseLimitType { get; set; }
        //public decimal CostMoneyCanUse { get; set; }
        //public ExpiryDateTypes ExpiryDateType { get; set; }
        //Fixed = 1,
        //Specified = 2,
        //public string[] FixedExpiryDateDescriptor { get; set; }
        //public decimal?[] SpecifiedExpiryDateDescriptor { get; set; }
        ////public ApplyScopes SpendScope { get; set; }
        //public string[] AllowShops { get; set; }
        public CardCouponStates State { get; set; }
    }
    //public class CashSpecific
    //{
    //    public float? LeastCost { get; set; }
    //    public float? ReduceCost { get; set; }
    //}
    //public class DiscountSpecific
    //{
    //    public string Discount { get; set; }
    //}
    //public class GiftSpecific
    //{
    //    public string Detail { get; set; }
    //}
    //public class GrounponSpecific
    //{
    //    public decimal? DValue { get; set; }
    //    public decimal? SaleValue { get; set; }
    //    public string Detail { get; set; }
    //}
    //public class GeneralCouponSpecific
    //{
    //    public string DefaultDetail { get; set; }
    //}
}


namespace Enjoy.Core.ViewModels
{
    using System;

    using System.Linq;
    using Enjoy.Core.EnjoyModels;
    using Enjoy.Core.WeChatModels;

    public class CardCounponViewModel
    {
        public CardCounponViewModel(long merchantid, CardTypes type)
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
                Merchant = new SubMerchantInfo() { MerchantId = 0 },
                Dateinfo = new DateInfo()
                {
                    Type = type == CardTypes.MEMBER_CARD ? ExpiryDateTypes.DATE_TYPE_PERMANENT.ToString() : ExpiryDateTypes.DATE_TYPE_FIX_TIME_RANGE.ToString(),

                    //BeginTimestamp = DateTime.UtcNow.ToUnixStampDateTime(),
                    //EndTimestamp = DateTime.UtcNow.AddMonths(1).ToUnixStampDateTime(),
                },
                Color = EnjoyConstant.CouponBackgroundColors.Values.FirstOrDefault(),
                Getlimit = 1,

                CenterTitle = "立即使用",
                CenterUrl = "https://www.yourc.club/",//打开商户小程序的Url,
            };
            this.Cash = new CashSpecific();
            this.Discount = new DiscountSpecific();
            this.Gift = new GiftSpecific();
            this.General = new GeneralCouponSpecific();
            this.Groupon = new GrounponSpecific();
            this.FixedExpiryDateDescriptor = new string[] {
              string.Empty,
              string.Empty
            };
            this.BaseInfo.CodeType = CodeTypes.CODE_TYPE_QRCODE.ToString();
            this.AdvancedInfo.UseCondition.CanUseWithOtherDiscount = false;
            this.BaseInfo.CanShare = true;
            this.BaseInfo.CanGivefriend = true;
            this.MerberCard = new MerberCardWapper() { };
            this.MerchantId = merchantid;
            this.CardType = type;
        }
        public CardCounponViewModel() { }
        public long Id { get; set; }
        public long MerchantId { get; set; }
        public string WxNo { get; set; }
        public CardTypes CardType { get; set; }
        public BaseInfo BaseInfo { get; set; }
        public AdvancedInfo AdvancedInfo { get; set; }
        public CashSpecific Cash { get; set; }
        public DiscountSpecific Discount { get; set; }
        public GiftSpecific Gift { get; set; }
        public GrounponSpecific Groupon { get; set; }
        public GeneralCouponSpecific General { get; set; }
        public MerberCardWapper MerberCard { get; set; }
        public ApplyScopes UseProductScope { get; set; }
        public ApplyScopes UseShopScope { get; set; }
        public long CreatedTime { get; set; }
        /// <summary>
        /// 使用限制 (使用条件)
        /// </summary>
        //public UseLimitTypes UseLimitType { get; set; }
        //public decimal CostMoneyCanUse { get; set; }
        //public ExpiryDateTypes ExpiryDateType { get; set; }
        //Fixed = 1,
        //Specified = 2,
        public string[] FixedExpiryDateDescriptor { get; set; }
        public decimal?[] SpecifiedExpiryDateDescriptor { get; set; }
        //public ApplyScopes SpendScope { get; set; }
        public string[] AllowShops { get; set; }
        public CardCouponStates CCStatus { get; set; }
    }
    public class CashSpecific
    {
        public int? LeastCost { get; set; }
        public int? ReduceCost { get; set; }
    }
    public class DiscountSpecific
    {
        public int? Discount { get; set; }
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


namespace Enjoy.Core
{

    using System;
    using Enjoy.Core.EnjoyModels;
    using Enjoy.Core.WeChatModels;
    using Enjoy.Core.Records;

    using System.Linq;
    using Enjoy.Core.ViewModels;
    using WeChatModels = Enjoy.Core.WeChatModels;
    public static class CardCouponExtension
    {
        public static ICardCoupon DeserializeSpecificCardCoupon(this string json, CardTypes type)
        {
            ICardCoupon cardCoupon = null;

            switch (type)
            {
                case CardTypes.CASH:
                    cardCoupon = json.DeserializeToObject<CashCoupon>();
                    break;
                case CardTypes.DISCOUNT:
                    cardCoupon = json.DeserializeToObject<DiscountCoupon>();
                    break;
                case CardTypes.GENERAL_COUPON:
                    cardCoupon = json.DeserializeToObject<GeneralCoupon>();
                    break;
                case CardTypes.GIFT:
                    cardCoupon = json.DeserializeToObject<GiftCoupon>();
                    break;
                case CardTypes.GROUPON:
                    cardCoupon = json.DeserializeToObject<Groupon>();
                    break;
                case CardTypes.MEMBER_CARD:
                    cardCoupon = json.DeserializeToObject<MemberCard>();
                    break;
            }
            if (cardCoupon != null)
            {
                cardCoupon.CardType = type;
                return cardCoupon;
            }
            throw new NotSupportedException(type.ToString());
        }
        public static string TextOf(this CardTypes type)
        {
            switch (type)
            {
                case CardTypes.CASH:
                    return "代金券";
                case CardTypes.DISCOUNT:
                    return "折扣券";
                case CardTypes.GENERAL_COUPON:
                    return "通用券";
                case CardTypes.GIFT:
                    return "礼品券";
                case CardTypes.GROUPON:
                    return "团购券";
                case CardTypes.MEMBER_CARD:
                    return "会员卡";
            }
            throw new NotSupportedException("No support with type " + type.ToString());
        }

        public static string WithDisplayName(this CardCouponStates status)
        {
            var text = status.ToString();
            return string.Join("|", text.Split(',').Select((o) =>
            {
                switch (o.Trim())
                {
                    case "Editing":
                        return "编辑中";
                    case "Checking":
                        return "审核中";
                    case "Approved":
                        return "审核通过";
                    case "Rejected":
                        return "审核失败";
                    case "RunOut":
                        return "已领完";
                    case "Expired":
                        return "已过期";
                    case "PublishedError":
                        return "发布出错";
                    default:
                        return "";
                }
            }));
        }
        public static bool Disabled(this CardTypes type, CardTypes current)
        {
            //卡券提交以后不可以更改卡券类型，因此 有此判断          
            return type != current;
        }
        public static bool Required(this CardTypes type, CardTypes current)
        {
            if (current == CardTypes.None) return false;
            return type == current;
        }
        public static string GetViewName(this CardTypes type)
        {
            switch (type)
            {
                case CardTypes.CASH:
                    return "Cash";
                case CardTypes.DISCOUNT:
                    return "Discount";
                case CardTypes.GIFT:
                    return "Gift";
                case CardTypes.MEMBER_CARD:
                    return "Member";
                default:
                    throw new NotSupportedException(type.ToString());

            }

        }
        public static CardCounponModel Initialize(this CardTypes type, MerchantModel merchant)
        {
            var model = new CardCounponModel();
            model.Merchant = merchant;
            model.CreatedTime = DateTime.Now.ToUnixStampDateTime();
            model.Type = type;
            switch (type)
            {
                case CardTypes.CASH:
                    model.CardCoupon = new CashCoupon();
                    break;
                case CardTypes.DISCOUNT:
                    model.CardCoupon = new DiscountCoupon();
                    break;
                case CardTypes.GENERAL_COUPON:
                    model.CardCoupon = new GeneralCoupon();
                    break;
                case CardTypes.GIFT:
                    model.CardCoupon = new GiftCoupon();
                    break;
                case CardTypes.GROUPON:
                    model.CardCoupon = new Groupon();
                    break;
                case CardTypes.MEMBER_CARD:
                    model.CardCoupon = new MemberCard()
                    {
                        SupplyBonus = false,                        
                        AutoActivate = false,
                        BonusRule = new BonusRule(),
                        CustomCell = new CustomCell(),
                        CustomField1 = new CustomField() { },
                        Discount = 10,
                        Prerogative = string.Empty,
                        SupplyBanlance = false,
                        WxActivate = true
                    };
                    break;
            }
            model.CardCoupon.AdvancedInfo = new AdvancedInfo().WithInitializeSettings(type, merchant);
            model.CardCoupon.BaseInfo = new BaseInfo().WithInitializeSettings(type, merchant);
            model.CardCoupon.CardId = string.Empty;
            model.CardCoupon.CardType = type;
            return model;
        }

        public static CardCounponViewModel Convert(this CardCounponModel card)
        {
            var viewModel = new CardCounponViewModel()
            {
                Cash = card.CardCoupon.ToCardCoupon<CashCoupon>(),
                Discount = card.CardCoupon.ToCardCoupon<DiscountCoupon>(),
                General = card.CardCoupon.ToCardCoupon<GeneralCoupon>(),
                Groupon = card.CardCoupon.ToCardCoupon<Groupon>(),
                Gift = card.CardCoupon.ToCardCoupon<GiftCoupon>(),
                MerberCard = card.CardCoupon.ToCardCoupon<MemberCard>(),
                CreatedTime = card.CreatedTime,
                MerchantId = card.Merchant.Id,
                WxNo = card.WxNo,
                CardType = card.Type,
                Id = card.Id,
                State = card.State,
                SubMerchantBrandName = card.Merchant.BrandName,
            };
            return viewModel;
        }
        public static T ToCardCoupon<T>(this ICardCoupon card)
            where T : class
        {
            return card as T;
        }
        public static CardCounponModel Convert(this CardCounponViewModel viewModel, MerchantModel merchant)
        {
            var model = new CardCounponModel()
            {
                BrandName = viewModel.Choose<ICardCoupon>().BaseInfo.Title,
                CreatedTime = viewModel.CreatedTime,
                LastUpdateTime = DateTime.Now.ToUnixStampDateTime(),
                ErrMsg = string.Empty,
                Id = viewModel.Id,
                Merchant = merchant,
                Quantity = (int)viewModel.Choose<ICardCoupon>().BaseInfo.Sku.Quantity,
                State = viewModel.State,
                Type = viewModel.CardType,
                WxNo = viewModel.WxNo
            };

            viewModel.Choose<ICardCoupon>().AdvancedInfo.WitFixedSettings(viewModel.CardType);
            viewModel.Choose<ICardCoupon>().BaseInfo.WithFixedSettings(viewModel.CardType, merchant);

            model.CardCoupon = viewModel.Choose();
            model.CardCoupon.Set((ctx) =>
            {
                ctx.CardType = viewModel.CardType;
                ctx.CardId = viewModel.WxNo;
            });
            var json = model.CardCoupon.ToJson();
            return model;
        }

        public static T Choose<T>(this CardCounponViewModel viewModel) where T : class, ICardCoupon
        {
            switch (viewModel.CardType)
            {
                case CardTypes.CASH:
                    return viewModel.Cash as T;
                case CardTypes.DISCOUNT:
                    return viewModel.Discount as T;
                case CardTypes.GENERAL_COUPON:
                    return viewModel.General as T;
                case CardTypes.GIFT:
                    return viewModel.Gift as T;
                case CardTypes.GROUPON:
                    return viewModel.Groupon as T;
                case CardTypes.MEMBER_CARD:
                    return viewModel.MerberCard as T;
                default:
                    throw new NotSupportedException(viewModel.CardType.ToString());

            }
        }
        public static CardCounponViewModel WithFixedSettings(this CardCounponViewModel viewModel)
        {
            switch (viewModel.CardType)
            {
                case CardTypes.MEMBER_CARD:                   
                    break;
            }
            return viewModel;
        }
        public static ICardCoupon Choose(this CardCounponViewModel viewModel)
        {

            switch (viewModel.CardType)
            {
                case CardTypes.CASH:
                    return viewModel.Cash;
                case CardTypes.DISCOUNT:
                    return viewModel.Discount;
                case CardTypes.GENERAL_COUPON:
                    return viewModel.General;
                case CardTypes.GIFT:
                    return viewModel.Gift;
                case CardTypes.GROUPON:
                    return viewModel.Groupon;
                case CardTypes.MEMBER_CARD:
                    {
                        if (viewModel.MerberCard.SupplyBanlance)
                        {
                            viewModel.MerberCard.BaseInfo.Merchant = null;
                        }
                        return viewModel.MerberCard;
                    }
                default:
                    throw new NotSupportedException(viewModel.CardType.ToString());

            }
        }
        public static BaseInfo WithInitializeSettings(this BaseInfo info, CardTypes type, MerchantModel merchant)
        {

            info.LogoUrl = merchant.LogoUrl;
            info.BrandName = merchant.BrandName;
            info.Title = type.TextOf();
            info.Color = Constants.CouponBackgroundColors["Color010"];
            info.Notice = "消费时向店员出示卡/券二维码";
            info.ServicePhone = merchant.Mobile;
            info.Description = "可与他人共享";
            info.Dateinfo = type == CardTypes.MEMBER_CARD
                ? new DateInfo() { Type = ExpiryDateTypes.DATE_TYPE_PERMANENT.ToString() }
                : new DateInfo() { Type = ExpiryDateTypes.DATE_TYPE_FIX_TERM.ToString() };


            info.CodeType = CodeTypes.CODE_TYPE_QRCODE.ToString();
            info.Sku = new Sku() { Quantity = 100 };
            if (type == CardTypes.MEMBER_CARD)
            {
                info.Uselimit = null;
            }
            else
            {
                info.Uselimit = 1;
            }
            info.Getlimit = 50;
            info.UseCustomCode = false;
            info.BindOpenid = true;
            info.CanGivefriend = true;
            info.LocationIdList = new long[] { };////TODO 需要搞清楚这些ID是怎么回事                  
            info.Merchant = new SubMerchantInfo() { MerchantId = merchant.MerchantId ?? 0 };

            return info;
        }
        public static BaseInfo WithFixedSettings(this BaseInfo info, CardTypes type, MerchantModel merchant)
        {
            info.BrandName = merchant.BrandName;
            info.LogoUrl = merchant.LogoUrl;


            info.CenterSubTitle = string.Empty;
            info.CenterAppBrandUserName = "gh_e1543e2be86d@app";
            info.CenterAppBrandPass = "pages/store/index";
            info.Merchant = new SubMerchantInfo()
            {
                MerchantId = merchant.MerchantId ?? 0
            };

            info.CustomAppBrandUserName = "gh_e1543e2be86d@app";
            info.CustomAppBrandPass = "pages/store/index";
            info.CustomUrlSubTitle = "分享赚积分";
            info.CustomUrlName = "分享";

            info.PromotionUrlName = "更多优惠";
            info.PromotionAppBrandUserName = "gh_e1543e2be86d@app";
            info.PromotionAppBrandPass = "pages/store/index";
            info.CanGivefriend = true;
            info.CanShare = false;
            info.BindOpenid = false;
            info.UseCustomCode = false;

            info.CodeType = CodeTypes.CODE_TYPE_ONLY_QRCODE.ToString();
            info.ServicePhone = merchant.Mobile;
            if (type == CardTypes.MEMBER_CARD)
            {
                info.CenterTitle = "立即买单";
                info.Dateinfo = new DateInfo() { Type = ExpiryDateTypes.DATE_TYPE_PERMANENT.ToString() };
                info.PayInfo = new PayInfo() { SwipeCard = new SwipeCard() { IsSwipeCard = true } };
            }
            else
            {
                info.CenterTitle = "立即使用";
            }
            return info;
        }
        public static AdvancedInfo WithInitializeSettings(this AdvancedInfo info, CardTypes type, MerchantModel merchant)
        {
            info.UseCondition = new UseCondition()
            {
                AcceptCategory = string.Empty,
                CanUseWithOtherDiscount = false,
                LeastCost = null,
                RejectCategory = string.Empty
            };
            info.Abstract = new Abstract()
            {
                AbstractX = string.Empty,
                IconUrlList = new string[] { string.Empty }
            };
            info.TextImageList = new System.Collections.Generic.List<TextImage>() {
                 new TextImage(){ },
                 new TextImage(){ },
                 new TextImage(){ }
            };

            info.BusinessService = Constants.BusinessService.Keys.ToArray();
            info.Abstract = new Abstract() { };
            return info;

        }
        public static AdvancedInfo WitFixedSettings(this AdvancedInfo info, CardTypes type)
        {
            switch (type)
            {
                case CardTypes.MEMBER_CARD:
                    break;
            }

            if (info.UseCondition == null)
                info.UseCondition = new UseCondition() { };
            info.UseCondition.CanUseWithOtherDiscount = false;
            return info;
        }
        public static CreatingWapper GenreateCreatingWapper(this ICardCoupon model)
        {

            switch (model.CardType)
            {
                case CardTypes.CASH:
                    return new CreatingWapper()
                    {
                        Card = new CashWapper()
                        {
                            Card = model as CashCoupon,
                            CardId = model.CardId,
                            CardType = model.CardType
                        }
                    };
                case CardTypes.DISCOUNT:
                    return new CreatingWapper()
                    {
                        Card = new DiscountWapper()
                        {
                            Card = model as DiscountCoupon,
                            CardId = model.CardId,
                            CardType = model.CardType
                        }
                    };
                case CardTypes.GENERAL_COUPON:
                    return new CreatingWapper()
                    {
                        Card = new GeneralWapper()
                        {
                            Card = model as GeneralCoupon,
                            CardId = model.CardId,
                            CardType = model.CardType
                        }
                    };
                case CardTypes.GIFT:
                    return new CreatingWapper()
                    {
                        Card = new GiftWapper()
                        {
                            Card = model as GiftCoupon,
                            CardId = model.CardId,
                            CardType = model.CardType
                        }
                    };

                case CardTypes.GROUPON:
                    return new CreatingWapper()
                    {
                        Card = new GrouponWapper()
                        {
                            Card = model as Groupon,
                            CardId = model.CardId,
                            CardType = model.CardType
                        }
                    };
                case CardTypes.MEMBER_CARD:
                    return new CreatingWapper()
                    {
                        Card = new MemberCardWapper()
                        {
                            Card = model as MemberCard,
                            CardId = model.CardId,
                            CardType = model.CardType
                        }
                    };
                default:
                    throw new NotSupportedException(model.CardType.ToString());

            }
        }

        public static UpgradeWapper GenreateUpgradeWpper(this ICardCoupon model)
        {

            switch (model.CardType)
            {
                case CardTypes.CASH:
                    return new UpgradeWapper()
                    {
                        CardId = model.CardId,
                        Cash = model.TransformForUpgrade() as CashCoupon
                    };
                case CardTypes.DISCOUNT:
                    return new UpgradeWapper()
                    {
                        CardId = model.CardId,
                        Discount = model.TransformForUpgrade() as DiscountCoupon
                    };
                case CardTypes.GENERAL_COUPON:
                    return new UpgradeWapper()
                    {
                        CardId = model.CardId,
                        Discount = model.TransformForUpgrade() as DiscountCoupon
                    };
                case CardTypes.GIFT:
                    return new UpgradeWapper()
                    {
                        CardId = model.CardId,
                        Gift = model.TransformForUpgrade() as GiftCoupon
                    };
                case CardTypes.GROUPON:
                    return new UpgradeWapper()
                    {
                        CardId = model.CardId,
                        Groupon = model.TransformForUpgrade() as Groupon
                    };

                case CardTypes.MEMBER_CARD:
                    return new UpgradeWapper()
                    {
                        CardId = model.CardId,
                        MemberCard = model.TransformForUpgrade() as MemberCard
                    };
                default:
                    throw new NotSupportedException(model.CardType.ToString());

            }
        }


    }
}
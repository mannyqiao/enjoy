

namespace Enjoy.Core
{
    using Enjoy.Core;
    using System;
    using WeChat.Models;
    using Enjoy.Core.Models;
    using System.Linq;
    public static class CardCouponExtension
    {
        public static WxCardCouponWapper<ICardCoupon> DeserializeSpecificCardCoupon(this string json, CardTypes type)
        {
            switch (type)
            {
                case CardTypes.CASH:
                    {
                        var cash = json.DeserializeToObject<WxCardCouponWapper<CashCoupon>>();
                        return new WxCardCouponWapper<ICardCoupon>()
                        {
                            Card = cash.Card
                        };
                    }
                case CardTypes.DISCOUNT:
                    {
                        var discount = json.DeserializeToObject<WxCardCouponWapper<DiscountCoupon>>();
                        return new WxCardCouponWapper<ICardCoupon>()
                        {
                            Card = discount.Card
                        };
                    }
                case CardTypes.GENERAL_COUPON:
                    {
                        var general = json.DeserializeToObject<WxCardCouponWapper<GeneralCoupon>>();
                        return new WxCardCouponWapper<ICardCoupon>()
                        {
                            Card = general.Card
                        };
                    }
                case CardTypes.GIFT:
                    {
                        var gift = json.DeserializeToObject<WxCardCouponWapper<GiftCoupon>>();
                        return new WxCardCouponWapper<ICardCoupon>()
                        {
                            Card = gift.Card
                        };
                    }
                //return json.DeserializeToObject<GiftCoupon>();
                case CardTypes.GROUPON:
                    {
                        var groupon = json.DeserializeToObject<WxCardCouponWapper<Groupon>>();
                        return new WxCardCouponWapper<ICardCoupon>()
                        {
                            Card = groupon.Card
                        };
                    }
                case CardTypes.MEMBER_CARD:
                    {
                        var membercard = json.DeserializeToObject<WxCardCouponWapper<MemberCard>>();
                        return new WxCardCouponWapper<ICardCoupon>()
                        {
                            Card = membercard.Card
                        };
                    }
            }
            throw new NotSupportedException("No support with type " + type.ToString());
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

        public static string TextOf(this CCStatus status)
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
    }
}
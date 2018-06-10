

namespace Enjoy.Core
{
    using Enjoy.Core;
    using System;
    using WeChat.Models;

    public static class CardCouponExtension
    {
        public static ICardCoupon DeserializeSpecificCardCoupon(this string json, CardTypes type)
        {
            switch (type)
            {
                case CardTypes.CASH:
                    return json.DeserializeToObject<CashCoupon>();
                case CardTypes.DISCOUNT:
                    return json.DeserializeToObject<DiscountCoupon>();                    
                case CardTypes.GENERAL_COUPON:
                    return json.DeserializeToObject<GeneralCoupon>();                    
                case CardTypes.GIFT:
                    return json.DeserializeToObject<GiftCoupon>();                
                case CardTypes.GROUPON:
                    return json.DeserializeToObject<Groupon>();
                case CardTypes.MEMBER_CARD:
                    throw new NotImplementedException();
            }
            throw new NotSupportedException("No support with type " + type.ToString());
        }
        public static string ToDisplayName(this CardTypes type)
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
    }
}
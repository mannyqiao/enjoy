
namespace Enjoy.Core
{
    using Enjoy.Core.EnjoyModels;
    using Enjoy.Core.WeChatModels;
    using System;
    public static class WeChatEventArgsExtension
    {
        public static WxUserCardCouponModel Generate(this GetCardCouponWeChatEventArgs arg,
            CardCounponModel model, long ownWxUser, long? gotfrom, bool gifting = false)
        {
            var result = new WxUserCardCouponModel()
            {
                FriendUserName = arg.FriendUserName,
                Gotfrom = gotfrom == null
                ? null
                : new WxUserModel() { Id = gotfrom.Value },
                IsGiveByFriend = arg.IsGiveByFriend,
                LastActivityTime = DateTime.Now.ToUnixStampDateTime(),
                Merchant = model.Merchant,
                IsGiftingToFriend = gifting,
                OldUserCardCode = arg.OldUserCardCode,
                CardCounpon = model,
                Owner = new WxUserModel() { Id = ownWxUser },
                Type = model.Type,
                UserCardCode = arg.UserCardCode
            };
            return result;
        }
    }
}
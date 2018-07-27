
namespace Enjoy.Core
{
    using Enjoy.Core.Models;
    using System;
    public static class WeChatEventArgsExtension
    {
        public static WxUserCardCouponModel Generate(this GetCardCouponWeChatEventArgs arg,
            CardCounponModel model, long ownWxUser, long? gotfrom)            
        {
            var result = new WxUserCardCouponModel()
            {
                FriendUserName = arg.FriendUserName,
                Gotfrom = gotfrom == null
                ? null
                : new WxUserModel() { Key = gotfrom.Value },
                IsGiveByFriend = arg.IsGiveByFriend,
                LastActivityTime = DateTime.Now.ToUnixStampDateTime(),
                Merchant = model.Merchant,
                OldUserCardCode = arg.OldUserCardCode,
                CardCounpon = model,
                Owner = new WxUserModel() { Key = ownWxUser },
                Type = model.Type,
                UserCardCode = arg.UserCardCode
            };
            return result ;            
        }
    }
}
using Enjoy.Core.Models.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.EModels
{
    public class WxUserCardCouponModel
    {

        public long Id { get; set; }
        public MerchantModel Merchant { get; set; }
        public WxUserModel Owner { get; set; }
        public WxUserModel Gotfrom { get; set; }
        public CardCounponModel CardCounpon { get; set; }
        public string UserCardCode { get; set; }
        public string OldUserCardCode { get; set; }
        public bool IsGiveByFriend { get; set; }
        public string FriendUserName { get; set; }
        public CardCouponStates State { get; set; }
        public CardTypes Type { get; set; }
        /// <summary>
        /// 存放会员卡，或者优惠券特有信息
        /// </summary>
        public string ExtraInfo { get; set; }

        public long LastActivityTime { get; set; }
    }
}
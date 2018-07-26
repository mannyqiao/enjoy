using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Models.Records
{
    /// <summary>
    /// 用户卡券
    /// </summary>
    public class WxUserCardCoupon : IEntityKey<long>
    {
        public virtual long Id { get; set; }
        public virtual Merchant Merchant { get; set; }
        public virtual WxUser Owner { get; set; }
        public virtual WxUser Gotfrom { get; set; }
        public virtual CardCoupon OwnCardCoupon { get; set; }
        public virtual string UserCardCode { get; set; }
        public virtual string OldUserCardCode { get; set; }
        public virtual bool IsGiveByFriend { get; set; }
        public virtual string FriendUserName { get; set; }
        public virtual CardCouponStates State { get; set; }
        public virtual CardTypes Type { get; set; }
        /// <summary>
        /// 存放会员卡，或者优惠券特有信息
        /// </summary>
        public virtual string ExtraInfo { get; set; }

        public virtual long LastActivityTime { get; set; }

    }
}
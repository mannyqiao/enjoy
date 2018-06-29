using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Models.Records
{
    /// <summary>
    /// User's  coupon
    /// </summary>
    public class UserCoupon : IEntityKey<int>
    {
        public virtual int Id { get; set; }
        public virtual string UserCardCode { get; set; }
        public virtual CardCoupon CardCoupon { get; set; }
        public virtual string OldUserCardCode { get; set; }
        public virtual CardTypes Type { get; set; }
        public virtual WxUser Owner { get; set; }
        public virtual WxUser FromWxUser { get; set; }
        public virtual Merchant Merchant { get; set; }
        public virtual string OuterStr { get; set; }
        public virtual string UnionId { get; set; }
    }
}
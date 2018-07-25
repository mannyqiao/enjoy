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
        public virtual int MerchantId { get; set; }
        public virtual int CouponId { get; set; }
        public virtual string UserCardCode { get; set; }
        public virtual CouponStates State { get; set; }
        public virtual CardTypes Type { get; set; }
        
        
    }
}
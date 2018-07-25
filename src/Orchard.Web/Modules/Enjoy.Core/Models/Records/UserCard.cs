using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Models.Records
{
    /// <summary>
    /// User's member card
    /// </summary>
    public class UserCard : IEntityKey<int>
    {
        public virtual int Id { get; set; }
        public virtual Merchant Merchant { get; set; }
        public virtual WxUser WxUser { get; set; }
        public virtual CardCoupon CardCoupon { get; set; }
    }
}
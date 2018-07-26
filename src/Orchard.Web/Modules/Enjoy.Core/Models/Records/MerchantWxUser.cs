using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Models.Records
{
    public class MerchantWxUser : IEntityKey<long>
    {
        public virtual long Id { get; set; }
        public virtual Merchant Merchant { get; set; }
        public virtual WxUser WxUser { get; set; }
        public virtual string OpenId { get; set; }
        public virtual long CreatedTime { get; set; }
        public virtual long LastActivityTime { get; set; }
    }

}
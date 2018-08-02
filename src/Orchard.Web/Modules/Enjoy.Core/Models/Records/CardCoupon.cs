using Orchard.Data.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Records
{
    public class CardCoupon : IEntityKey<long>
    {
        public virtual long Id { get; set; }
        public virtual string BrandName { get; set; }
        public virtual Merchant Merchant { get; set; }
        public virtual CardTypes Type { get; set; }
        public virtual string WxNo { get; set; }
        public virtual int Quantity { get; set; }        
        public virtual CCStatus Status { get; set; }
        public virtual string ErrMsg { get; set; }
        [StringLengthMax]
        public virtual string JsonMetadata { get; set; }
        public virtual long CreatedTime { get; set; }
        public virtual long LastActivityTime { get; set; }
    }
}
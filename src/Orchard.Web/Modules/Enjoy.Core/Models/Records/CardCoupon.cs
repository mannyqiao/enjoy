using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Models.Records
{
    public class CardCoupon : IEntityKey<int>
    {
        public virtual int Id { get; set; }
        public virtual string BrandName { get; set; }
        public virtual Merchant Merchant { get; set; }

        public virtual CardTypes Type { get; set; }
        public virtual string WxNo { get; set; }
        public virtual int Quantity { get; set; }
        public virtual long CreatedTime { get; set; }
        public virtual long LastUpdateTime { get; set; }
        public virtual string JsonMetadata { get; set; }
    }
}
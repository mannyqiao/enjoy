using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Records
{
    public class CardCouponNearby
    {
        public virtual long Id { get; set; }
        public virtual string WxNo { get; set; }
        public virtual string Merchant { get; set; }
        public virtual string BrandName {get;set;}
        public virtual string Privilege { get; set; }

        public virtual string LogoUrl { get; set; }
        public virtual float Latitude { get; set; }
        public virtual float Longitude { get; set; }      
        /// <summary>
        /// 距离 单位(千米)
        /// </summary>
        //public virtual float Distance { get; set; }
    }
}
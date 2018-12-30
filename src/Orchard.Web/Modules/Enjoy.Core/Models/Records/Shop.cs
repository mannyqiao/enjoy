using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Records
{
    public class Shop : IEntityKey<long>
    {
        public virtual long Id { get; set; }
        /// <summary>
        /// 来自微信的门店Id
        /// </summary>
        public virtual long Pid { get; set; }

        public virtual Merchant Merchant { get; set; }

        public virtual string ShopName { get; set; }

        public virtual string Leader { get; set; }

        public virtual string Address { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public virtual float Longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public virtual float Latitude { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual long LastActivityTime { get; set; }
      
    }
}
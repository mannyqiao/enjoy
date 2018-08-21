using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Records
{
    public class Product : IEntityKey<long>
    {
        public virtual long Id { get; set; }

        public virtual string Name { get; set; }

        public virtual Category Category { get; set; }
        //单价
        public virtual decimal Price { get; set; }
        public virtual Merchant Merchant { get; set; }
        /// <summary>
        /// 交易次数
        /// </summary>
        public virtual int Trades { get; set; }
      
        public virtual string Settings { get; set; }

        public long LastActivityTime { get; set; }
    }
}
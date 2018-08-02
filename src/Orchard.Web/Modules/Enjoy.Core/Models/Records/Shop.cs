using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Records
{
    public class Shop : IEntityKey<long>
    {
        public virtual long Id { get; set; }
        public virtual Merchant Merchant { get; set; }
        public virtual string ShopName { get; set; }
        public virtual string Leader { get; set; }
        public virtual string Address { get; set; }
        public virtual string Coordinate { get; set; }
        public virtual long LastActivityTime { get; set; }
    }
}
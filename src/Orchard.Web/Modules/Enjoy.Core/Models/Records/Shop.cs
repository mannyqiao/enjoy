using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Models.Records
{
    public class Shop
    {
        public virtual int Id { get; set; }
        public virtual Merchant Merchant { get; set; }
        public virtual string ShopName { get; set; }
        public virtual string Leader { get; set; }
        public virtual string Address { get; set; }
        public virtual string Coordinate { get; set; }    
    }
}
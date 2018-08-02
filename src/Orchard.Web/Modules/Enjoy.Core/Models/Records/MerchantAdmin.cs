using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Records
{
    public class MerchantAdmin
    {
        public virtual long Id { get; set; }
        public virtual EnjoyUser EnjoyUser { get; set; }
        public virtual Merchant Merchant { get; set; }
    }
}
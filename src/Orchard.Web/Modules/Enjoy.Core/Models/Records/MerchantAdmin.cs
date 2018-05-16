using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Models.Records
{
    public class MerchantAdmin
    {
        public virtual EnjoyUser EnjoyUser { get; set; }
        public virtual Merchant Merchant { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.WeChatModels
{
    public class JsApiPay
    {
        public string OpenId { get; set; }
        public string UnionId { get; set; }
        public int? TotalFee { get; set; }
    }
}
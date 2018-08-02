using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.WeChatModels
{
    public class DeleteCardCouponWeChatEventArgs : WeChatEventArgs
    {
        public string UserCardCode { get; set; }

    }
}
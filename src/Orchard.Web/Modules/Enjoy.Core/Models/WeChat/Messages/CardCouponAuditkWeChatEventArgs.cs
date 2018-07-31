using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Enjoy.Core.Models
{
    /// <summary>
    /// 卡券审核事件
    /// </summary>
    
    public class CardCouponAuditkWeChatEventArgs : WeChatEventArgs
    {
        public string RefuseReason { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Models
{
    public abstract class WeChatEventArgs : WeChatMsgModel
    {
        public EventTypes Event { get; set; }
        public string CardId { get; set; }
    }
}
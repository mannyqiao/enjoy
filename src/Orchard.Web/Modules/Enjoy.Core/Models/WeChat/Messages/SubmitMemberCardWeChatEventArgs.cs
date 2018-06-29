using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Models
{
    public class SubmitMemberCardWeChatEventArgs : WeChatEventArgs
    {
        public string UserCardCode { get; set; }
    }
}
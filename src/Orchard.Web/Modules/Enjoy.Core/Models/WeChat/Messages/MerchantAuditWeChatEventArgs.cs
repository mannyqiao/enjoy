using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Models
{
    public class MerchantAuditWeChatEventArgs : WeChatEventArgs
    {
        public int MerchantId { get; set; }
        public int IsPass { get; set; }
        public string Reason { get; set; }

        //        <xml>
        //<ToUserName>< ![CDATA[toUser] ]></ToUserName>
        // <FromUserName>< ![CDATA[FromUser] ]></FromUserName>
        // <CreateTime>123456789</CreateTime>
        // <MsgType>< ![CDATA[event] ]></MsgType>
        // <Event>< ![CDATA[card_merchant_check_result] ]></Event>
        // <MerchantId>0</MerchantId>
        // <IsPass>1</IsPass>
        // <Reason>< ![CDATA[reason] ]></Reason>
        //</xml>
    }
}
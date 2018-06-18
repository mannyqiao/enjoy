﻿

namespace Enjoy.Core.Models
{
    public class SkuRemindWeChatEventArgs : WeChatEventArgs
    {
        public string Detail { get; set; }

        //<xml>
        //   <ToUserName><![CDATA[gh_2d62d*****0]]></ToUserName>
        //    <FromUserName><![CDATA[oa3LFuBvWb7*********]]></FromUserName>
        //    <CreateTime>1443838506</CreateTime>
        //    <MsgType><![CDATA[event]]></MsgType>
        //    <Event><![CDATA[card_sku_remind]]></Event>
        //    <CardId><![CDATA[pa3LFuAh2P65**********]]></CardId>
        //    <Detail><![CDATA[the card's quantity is equal to 0]]></Detail>
        //</xml>
    }
}
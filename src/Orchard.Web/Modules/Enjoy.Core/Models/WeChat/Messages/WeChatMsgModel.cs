using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
///https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1451025274
namespace Enjoy.Core.Models
{

    public abstract class WeChatMsgModel
    {
        public string FromUserName { get; set; }
        public string ToUserName { get; set; }
        
        public long CreateTime { get; set; }

        public MsgTypes MsgType { get; set; }
        

        //        <xml>
        //   <ToUserName><![CDATA[toUser]]></ToUserName>
        //    <FromUserName><![CDATA[FromUser]]></FromUserName>
        //    <CreateTime>123456789</CreateTime>
        //    <MsgType><![CDATA[event]]></MsgType>
        //    <Event><![CDATA[card_pass_check]]></Event> //不通过为card_not_pass_check
        //   <CardId><![CDATA[cardid]]></CardId>
        //    <RefuseReason><![CDATA[非法代制]]></RefuseReason> 
        //</xml>
    }
}
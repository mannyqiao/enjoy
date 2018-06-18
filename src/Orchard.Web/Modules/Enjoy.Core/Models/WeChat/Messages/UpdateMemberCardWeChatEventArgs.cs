using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Models
{
    public class UpdateMemberCardWeChatEventArgs : WeChatEventArgs
    {
        public string UserCardCode { get; set; }

        public string ModifyBonus { get; set; }
        public string ModifyBalance { get; set; }
//<xml>
//  <ToUserName><![CDATA[gh_9e1765b5568e]]></ToUserName>
//    <FromUserName><![CDATA[ojZ8YtyVyr30HheH3CM73y7h4jJE]]></FromUserName>
//    <CreateTime>1445507140</CreateTime>
//    <MsgType><![CDATA[event]]></MsgType>
//    <Event><![CDATA[update_member_card]]></Event>
//    <CardId><![CDATA[pjZ8Ytx-nwvpCRyQneH3Ncmh6N94]]></CardId>
//    <UserCardCode><![CDATA[485027611252]]></UserCardCode>
//    <ModifyBonus>3</ModifyBonus>
//    <ModifyBalance>0</ModifyBalance> 
//</xml>
    }
}
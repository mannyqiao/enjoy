﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Models
{
    public class ViewCardWeChatEventArgs : WeChatEventArgs
    {

        public string UserCardCode { get; set; }
        public string OuterStr { get; set; }
//        <xml>
//    <ToUserName> <![CDATA[gh_fcxxxx6a20993]]> </ToUserName>
//    <FromUserName> <![CDATA[oZI8Fj040-xxxxx6gkoPOQTQ]]> </FromUserName>
//    <CreateTime>1467811138</CreateTime>
//    <MsgType> <![CDATA[event]]> </MsgType>
//    <Event> <![CDATA[user_view_card]]> </Event>
//    <CardId> <![CDATA[pZI8Fj2ezBbxxxxxT2UbiiWLb7Bg]]> </CardId>
//    <UserCardCode> <![CDATA[4xxxxxxxx8558]]> </UserCardCode>
//    <OuterStr> <![CDATA[12b]]> </OuterStr> 
//</xml>
    }
}
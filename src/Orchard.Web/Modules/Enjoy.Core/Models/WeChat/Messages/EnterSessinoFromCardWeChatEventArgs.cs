﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Models
{
    public class EnterSessinoFromCardWeChatEventArgs : WeChatEventArgs
    {
        public string UserCardCode { get; set; }
//        <xml>
//   <ToUserName><![CDATA[toUser]]></ToUserName>
//    <FromUserName><![CDATA[FromUser]]></FromUserName>
//    <CreateTime>123456789</CreateTime>
//    <MsgType><![CDATA[event]]></MsgType>
//    <Event><![CDATA[user_enter_session_from_card]]></Event>
//    <CardId><![CDATA[cardid]]></CardId>
//    <UserCardCode><![CDATA[12312312]]></UserCardCode> 
//</xml>
    }
}
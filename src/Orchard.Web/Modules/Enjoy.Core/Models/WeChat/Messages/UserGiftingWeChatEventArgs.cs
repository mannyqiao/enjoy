﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Enjoy.Core.WeChatModels
{
    /// <summary>
    /// 转赠事件
    /// </summary>
    [XmlRoot("xml")]
    public class UserGiftingWeChatEventArgs : WeChatEventArgs
    {
        public string UserCardCode { get; set; }
        public int IsReturnBack { get; set; }

        public string FriendUserName { get; set; }

        public int IsChatRoom { get; set; }
    }


//    <xml>
//  <ToUserName><![CDATA[gh_3fcea188bf78]]></ToUserName>  
//  <FromUserName><![CDATA[obLatjjwDolFjRRd3doGIdwNqRXw]]></FromUserName>  
//  <CreateTime>1474181868</CreateTime>  
//  <MsgType><![CDATA[event]]></MsgType>  
//  <Event><![CDATA[user_gifting_card]]></Event>  
//  <CardId><![CDATA[pbLatjhU-3pik3d4PsbVzvBxZvJc]]></CardId>  
//  <UserCardCode><![CDATA[297466945104]]></UserCardCode>  
//  <IsReturnBack>0</IsReturnBack>  
//  <FriendUserName><![CDATA[obLatjlNerkb62HtSdQUx66C4NTU]]></FriendUserName>  
//  <IsChatRoom>0</IsChatRoom> 
//</xml>
}
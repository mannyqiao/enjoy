

namespace Enjoy.Core.WeChatModels
{
    using System.Xml.Serialization;
    [XmlRoot("xml")]
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
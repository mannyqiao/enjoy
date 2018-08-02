

namespace Enjoy.Core.EModels
{
    public class ConsumCardCouponWeChatArgs : WeChatEventArgs
    {
        public string UserCardCode { get; set; }
        public string ConsumeSource { get; set; }

        public string LocationName { get; set; }
        public string StaffOpenId { get; set; }

        public string VerifyCode { get; set; }
        public string RemarkAmount { get; set; }
        public string OuterStr { get; set; }
    }

//    <xml>
//    <ToUserName> <![CDATA[gh_fc0a06a20993]]> </ToUserName>
//    <FromUserName> <![CDATA[oZI8Fj040-be6rlDohc6gkoPOQTQ]]> </FromUserName>
//    <CreateTime>1472549042</CreateTime>
//    <MsgType> <![CDATA[event]]> </MsgType>
//    <Event> <![CDATA[user_consume_card]]> </Event>
//    <CardId> <![CDATA[pZI8Fj8y-E8hpvho2d1ZvpGwQBvA]]> </CardId>
//    <UserCardCode> <![CDATA[452998530302]]> </UserCardCode>
//    <ConsumeSource> <![CDATA[FROM_API]]> </ConsumeSource>
//    <LocationName> <![CDATA[]]> </LocationName>
//    <StaffOpenId> <![CDATA[oZ********nJ3bPJu_Rtjkw4c]]> </StaffOpenId>
//    <VerifyCode> <![CDATA[]]> </VerifyCode>
//    <RemarkAmount> <![CDATA[]]> </RemarkAmount>
//    <OuterStr> <![CDATA[xxxxx]]> </OuterStr> 
//</xml>
}
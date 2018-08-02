using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.EModels
{
    /// <summary>
    /// 卡券领取事件
    /// </summary>
    public class GetCardCouponWeChatEventArgs : WeChatEventArgs
    {
        
        public bool IsGiveByFriend { get; set; }
        public string UserCardCode { get; set; }
        public string FriendUserName { get; set; }

        public int OuterId { get; set; }
        public string OldUserCardCode { get; set; }
        public string OuterStr { get; set; }

        public bool IsRestoreMemberCard { get; set; }
        /// <summary>
        /// 只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段。
        /// </summary>
        public string UnionId { get; set; }
        //        <xml>
        //   <ToUserName> <![CDATA[gh_fc0a06a20993]]> </ToUserName>
        //    <FromUserName> <![CDATA[oZI8Fj040-be6rlDohc6gkoPOQTQ]]> </FromUserName>
        //    <CreateTime>1472551036</CreateTime>
        //    <MsgType> <![CDATA[event]]> </MsgType>
        //    <Event> <![CDATA[user_get_card]]> </Event>
        //    <CardId> <![CDATA[pZI8Fjwsy5fVPRBeD78J4RmqVvBc]]> </CardId>
        //    <IsGiveByFriend>0</IsGiveByFriend>
        //    <UserCardCode> <![CDATA[226009850808]]> </UserCardCode>
        //    <FriendUserName> <![CDATA[]]> </FriendUserName>
        //    <OuterId>0</OuterId>
        //    <OldUserCardCode> <![CDATA[]]> </OldUserCardCode>
        //    <OuterStr> <![CDATA[12b]]> </OuterStr>
        //    <IsRestoreMemberCard>0</IsRestoreMemberCard>
        //    <IsRecommendByFriend>0</IsRecommendByFriend>
        //    <UnionId>o6_bmasdasdsad6_2sgVt7hMZOPfL</UnionId>
        //</xml>
    }
}
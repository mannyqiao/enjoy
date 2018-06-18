

namespace Enjoy.Core.Models
{
    public class PayFromPayCellWeChatEventArgs : WeChatEventArgs
    {

        public string UserCardCode { get; set; }
        public string TransId { get; set; }
        public string LocationId { get; set; }
        public string Fee { get; set; }
        public string OriginalFee { get; set; }
        //        <xml>
        //    <ToUserName><![CDATA[gh_e2243xxxxxxx]]></ToUserName>
        //    <FromUserName><![CDATA[oo2VNuOUuZGMxxxxxxxx]]></FromUserName>
        //    <CreateTime>1442390947</CreateTime>
        //    <MsgType><![CDATA[event]]></MsgType>
        //    <Event><![CDATA[user_pay_from_pay_cell]]></Event>
        //    <CardId><![CDATA[po2VNuCuRo-8sxxxxxxxxxxx]]></CardId>
        //    <UserCardCode><![CDATA[38050000000]]></UserCardCode>
        //    <TransId><![CDATA[10022403432015000000000]]></TransId>
        //    <LocationId>291710000</LocationId>
        //    <Fee><![CDATA[10000]]></Fee>
        //    <OriginalFee><![CDATA[10000]]> </OriginalFee> 
        //</xml>

//        参数 说明
//ToUserName 开发者微信号。
//FromUserName 发送方帐号（一个OpenID）。
//CreateTime 消息创建时间 （整型）。
//MsgType 消息类型，event。
//Event 事件类型，User_pay_from_pay_cell(微信买单事件)
//CardId  卡券ID。
//UserCardCode 卡券Code码。
//TransId 微信支付交易订单号（只有使用买单功能核销的卡券才会出现）
//LocationId 门店ID，当前卡券核销的门店ID（只有通过卡券商户助手和买单核销时才会出现）
//Fee 实付金额，单位为分
//OriginalFee 应付金额，单位为分
    }
}
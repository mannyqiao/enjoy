using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.EModels
{
    public class PayOrderWeChatEventArgs : WeChatEventArgs
    {
        public string OrderId { get; set; }
        /// <summary>
        /// 本次订单号的状态,
        /// ORDER_STATUS_WAITING 等待支付 
        /// ORDER_STATUS_SUCC 支付成功 
        /// ORDER_STATUS_FINANCE_SUCC 加代币成功 
        /// ORDER_STATUS_QUANTITY_SUCC 加库存成功 
        /// ORDER_STATUS_HAS_REFUND 已退币 
        /// ORDER_STATUS_REFUND_WAITING 等待退币确认 
        /// ORDER_STATUS_ROLLBACK 已回退,系统失败 ORDER_STATUS_HAS_RECEIPT 已开发票
        /// </summary>
        public string Status { get; set; }
        public long CreateOrderTime { get; set; }
        public long PayFinishTime { get; set; }
        public string Desc { get; set; }

        public int FreeCoinCount { get; set; }
        public int PayCoinCount { get; set; }
        public int RefundFreeCoinCount { get; set; }
        /// <summary>
        /// 所要拉取的订单类型 
        /// ORDER_TYPE_SYS_ADD 平台赠送券点 
        /// ORDER_TYPE_WXPAY 充值券点 
        /// ORDER_TYPE_REFUND 库存未使用回退券点 
        /// ORDER_TYPE_REDUCE 券点兑换库存 
        /// ORDER_TYPE_SYS_REDUCE 平台扣减
        /// </summary>
        public string OrderType { get; set; }
        public string Memo { get; set; }
        
        public string ReceiptInfo { get; set; }
//<xml>
//   <ToUserName><![CDATA[gh_7223c83d4be5]]></ToUserName>
//    <FromUserName><![CDATA[ob5E7s-HoN9tslQY3-0I4qmgluHk]]></FromUserName>
//    <CreateTime>1453295737</CreateTime>
//    <MsgType><![CDATA[event]]></MsgType>
//    <Event><![CDATA[card_pay_order]]></Event>
//    <OrderId><![CDATA[404091456]]></OrderId>
//    <Status><![CDATA[ORDER_STATUS_FINANCE_SUCC]]></Status>
//    <CreateOrderTime>1453295737</CreateOrderTime>
//    <PayFinishTime>0</PayFinishTime>
//    <Desc><![CDATA[]]></Desc>
//    <FreeCoinCount><![CDATA[200]]></FreeCoinCount>
//    <PayCoinCount><![CDATA[0]]></PayCoinCount>
//    <RefundFreeCoinCount><![CDATA[0]]></RefundFreeCoinCount>
//    <RefundPayCoinCount><![CDATA[0]]></RefundPayCoinCount>
//    <OrderType><![CDATA[ORDER_TYPE_SYS_ADD]]></OrderType>
//    <Memo><![CDATA[开通账户奖励]]></Memo>
//    <ReceiptInfo><![CDATA[]]></ReceiptInfo>
//</xml>
    }
}
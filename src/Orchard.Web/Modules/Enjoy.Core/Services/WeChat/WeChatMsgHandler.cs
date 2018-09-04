



namespace Enjoy.Core
{
    using Enjoy.Core.EnjoyModels;
    using Orchard;
    using Orchard.Logging;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Linq;
    using Enjoy.Core.Records;
    using Enjoy.Core.WeChatModels;
    public class WeChatMsgHandler : IWeChatMsgHandler
    {
        private IEnumerable<IWeChatEventBehavior> Behaviors;
        private IOrchardServices OS;
        public WeChatMsgHandler(
            IOrchardServices os,
            IEnumerable<IWeChatEventBehavior> behaviors)
        {
            this.Behaviors = behaviors;
            this.OS = os;
            this.Logger = NullLogger.Instance;
        }
        public ILogger Logger { get; private set; }
        public void Handle(IWxMsgToken token)
        {
            var crypt = new WXBizMsgCrypt(token);
            var reqMsg = string.Empty;
            var retVal = crypt.DecryptMsg(token, ref reqMsg);

            if (retVal== 0)//解密收到的消息内容
            {
                Handle(reqMsg);
            }
            else
            {
                Logger.Error("消息解密出错 error code: {0}", retVal);
            }
        }
        protected virtual void Completed()
        {
            this.OS.WorkContext.HttpContext.Response.Write(string.Empty);
            this.OS.WorkContext.HttpContext.Response.End();
        }
             
             
        public void Handle(string xmlMsg)
        {
            xmlMsg = xmlMsg.RepairXmlText();
            var document = new XmlDocument();
            document.LoadXml(xmlMsg);
            if (Enum.TryParse(document.SelectSingleNode("/xml/MsgType").InnerText, true, out MsgTypes msgType))
            {
                using (var reader = new StringReader(xmlMsg))
                {
                    if (msgType == MsgTypes.@event)
                    {
                        if (Enum.TryParse(document.SelectSingleNode("/xml/Event").InnerText, true, out EventTypes eventtype) == false)
                        {
                            eventtype = EventTypes.Nothing;
                        }
                        var model = new XmlSerializer(dictnoary[eventtype]).Deserialize(reader);
                        //保存解密后的消息文本
                        this.SaveWxMessageToken(model as WeChatMsgModel);

                        //根据不同的消息类型进行不同的处理
                        IWeChatEventBehavior behavior = this.Behaviors.FirstOrDefault(o => o.Type == eventtype);
                        behavior.Execute(model);
                    }
                }
            }
        }

        private void SaveWxMessageToken(WeChatMsgModel model)
        {
            var msg = new WxMsg()
            {
                LastActivityTime = model.CreateTime,
                FromUser = model.FromUserName,
                MsgType = model.MsgType,
                Metadata = model.SerializeToJson(),
                ToUser = model.ToUserName
            };
            this.OS.TransactionManager.GetSession().SaveOrUpdate(msg);

        }



        Dictionary<EventTypes, Type> dictnoary = new Dictionary<EventTypes, Type>()
        {
            { EventTypes.card_not_pass_check, typeof(CardCouponAuditkWeChatEventArgs) },
            { EventTypes.card_pass_check, typeof(CardCouponAuditkWeChatEventArgs) },
            { EventTypes.user_get_card, typeof(GetCardCouponWeChatEventArgs) },
            { EventTypes.user_gifting_card, typeof(UserGiftingWeChatEventArgs) },
            { EventTypes.user_del_card, typeof(DeleteCardCouponWeChatEventArgs) },
            { EventTypes.user_consume_card, typeof(ConsumCardCouponWeChatArgs) },
            { EventTypes.user_pay_from_pay_cell, typeof(PayFromPayCellWeChatEventArgs) },
            { EventTypes.user_view_card, typeof(ViewCardWeChatEventArgs) },
            { EventTypes.user_enter_session_from_card, typeof(EnterSessinoFromCardWeChatEventArgs) },
            { EventTypes.update_member_card, typeof(UpdateMemberCardWeChatEventArgs) },
            { EventTypes.card_sku_remind, typeof(SkuRemindWeChatEventArgs) },
            { EventTypes.card_pay_order, typeof(PayOrderWeChatEventArgs) },
            { EventTypes.submit_membercard_user_info, typeof(SubmitMemberCardWeChatEventArgs) },
            { EventTypes.card_merchant_check_result, typeof(MerchantAuditWeChatEventArgs) },
            { EventTypes.Nothing, typeof(DoNothingWeChatMsgModel) }
        };

    }
}
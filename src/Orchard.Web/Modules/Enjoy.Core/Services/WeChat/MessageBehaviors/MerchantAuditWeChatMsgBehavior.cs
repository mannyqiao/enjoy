using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Services
{
    using Orchard;
    using Enjoy.Core.Models;
    public class MerchantAuditWeChatMsgBehavior : WeChatMsgBehavior<MerchantAuditWeChatEventArgs>
    {
        private readonly IMerchantService Merchant;
        private readonly ISMSHelper SMSHelper;
        public MerchantAuditWeChatMsgBehavior(
            IOrchardServices os,
            IMerchantService merchant,
            ISMSHelper smsHelper) : base(os)
        {
            this.Merchant = merchant;
            this.SMSHelper = smsHelper;
        }

        public override EventTypes Type
        {
            get
            {
                return EventTypes.card_merchant_check_result;
            }
        }

        protected override void Execute(MerchantAuditWeChatEventArgs model)
        {
            //你的商户审核{1}. 登录 https://www.yourc.club/ 查看详情
            var merchant = this.Merchant.GetDefaultMerchantByWeChatMerchantId(model.MerchantId);
            this.SMSHelper.Send(new QCloudSMS(
                merchant.EnjoyUser.Mobile,
                SMSNotifyTypes.MerchantAudit,
                merchant.BrandName,
                model.IsPass == 0 ? "失败" : "成功"));

            merchant.Status = model.IsPass.Equals(1) ? AuditStatus.APPROVED : AuditStatus.REJECTED;
            merchant.ErrMsg = model.Reason;
            this.Merchant.SaveOrUpdate(merchant);            
        }
    }
}
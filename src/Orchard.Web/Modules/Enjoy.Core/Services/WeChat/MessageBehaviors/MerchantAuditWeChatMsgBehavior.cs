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
        public MerchantAuditWeChatMsgBehavior(
            IOrchardServices os,
            IMerchantService merchant) : base(os)
        {
            this.Merchant = merchant;
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
            this.Merchant.UpdateMerchantStatus(
                model.MerchantId,
                model.IsPass.Equals(1)
                ? AuditStatus.APPROVED
                : AuditStatus.REJECTED,
                model.Reason);
        }
    }
}
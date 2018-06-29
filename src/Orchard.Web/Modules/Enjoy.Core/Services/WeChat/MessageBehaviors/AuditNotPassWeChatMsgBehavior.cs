


namespace Enjoy.Core.Services
{
    using Enjoy.Core.Models;
    using Orchard;
    public class AuditNotPassWeChatMsgBehavior : WeChatMsgBehavior<CardCouponAuditkWeChatEventArgs>
    {
        public AuditNotPassWeChatMsgBehavior(IOrchardServices os)
            : base(os)
        {

        }
        public override EventTypes Type
        {
            get
            {
                return EventTypes.card_not_pass_check;
            }
        }

        protected override void Execute(CardCouponAuditkWeChatEventArgs model)
        {

        }
    }
}
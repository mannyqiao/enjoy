
namespace Enjoy.Core.Services
{
    using Enjoy.Core.Models;
    using Orchard;
    public class AuditPassWeChatMsgBehavior : WeChatMsgBehavior<CardCouponAuditkWeChatEventArgs>
    {

        public AuditPassWeChatMsgBehavior(IOrchardServices os)
            : base(os)
        {

        }
        public override EventTypes Type 
        {
            get
            {
                return EventTypes.card_pass_check;
            }
        }

        protected override void Execute(CardCouponAuditkWeChatEventArgs model)
        {

        }
    }
}
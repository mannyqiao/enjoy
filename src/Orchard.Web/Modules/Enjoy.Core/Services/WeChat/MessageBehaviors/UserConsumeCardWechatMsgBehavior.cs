

namespace Enjoy.Core.Services
{
    using Enjoy.Core.Models;
    using Orchard;
    
    public class UserConsumeCardWechatMsgBehavior : WeChatMsgBehavior<ConsumCardCouponWeChatArgs>
    {
        public UserConsumeCardWechatMsgBehavior(IOrchardServices os)
            : base(os)
        {
        }

        public override EventTypes Type
        {
            get
            {
                return EventTypes.user_consume_card;
            }
        }

        protected override void Execute(ConsumCardCouponWeChatArgs model)
        {
            //throw new System.NotImplementedException();
        }
    }
}
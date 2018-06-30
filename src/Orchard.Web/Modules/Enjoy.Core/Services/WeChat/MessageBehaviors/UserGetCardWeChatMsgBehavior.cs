

namespace Enjoy.Core.Services
{
    using Enjoy.Core.Models;
    using Orchard;

    public class UserGetCardWeChatMsgBehavior : WeChatMsgBehavior<GetCardCouponWeChatEventArgs>
    {
        public UserGetCardWeChatMsgBehavior(IOrchardServices os)
            : base(os)
        {
        }

        public override EventTypes Type
        {
            get
            {
                return EventTypes.user_get_card;
            }
        }

        protected override void Execute(GetCardCouponWeChatEventArgs model)
        {
            
        }
    }
}
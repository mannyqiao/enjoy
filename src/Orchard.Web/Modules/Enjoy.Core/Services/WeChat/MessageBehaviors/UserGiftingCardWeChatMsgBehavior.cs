

namespace Enjoy.Core.Services
{
    using Orchard;
    using Enjoy.Core.EnjoyModels;
    using Enjoy.Core.WeChatModels;

    public class UserGiftingCardWeChatMsgBehavior : WeChatMsgBehavior<UserGiftingWeChatEventArgs>
    {
        public UserGiftingCardWeChatMsgBehavior(IOrchardServices os)
            : base(os)
        {
        }

        public override EventTypes Type
        {
            get
            {
                return EventTypes.user_gifting_card;
            }
        }

        protected override void Execute(UserGiftingWeChatEventArgs model)
        {
            //throw new System.NotImplementedException();
        }
    }
}
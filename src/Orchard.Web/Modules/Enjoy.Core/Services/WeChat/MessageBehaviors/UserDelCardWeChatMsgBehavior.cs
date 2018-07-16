



namespace Enjoy.Core.Services
{
    using Enjoy.Core.Models;
    using Orchard;
    public class UserDelCardWeChatMsgBehavior : WeChatMsgBehavior<DeleteCardCouponWeChatEventArgs>
    {
        public UserDelCardWeChatMsgBehavior(IOrchardServices os)
            : base(os)
        {

        }

        public override EventTypes Type
        {
            get
            {
                return EventTypes.user_del_card;
            }
        }      

        protected override void Execute(DeleteCardCouponWeChatEventArgs model)
        {
            //throw new System.NotImplementedException();
        }
    }
}
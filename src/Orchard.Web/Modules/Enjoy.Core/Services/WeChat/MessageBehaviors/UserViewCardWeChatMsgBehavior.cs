
namespace Enjoy.Core.Services
{
    using Orchard;
    using Enjoy.Core.EnjoyModels;
    using Enjoy.Core.WeChatModels;

    public class UserViewCardWeChatMsgBehavior : WeChatMsgBehavior<ViewCardWeChatEventArgs>
    {
        public UserViewCardWeChatMsgBehavior(IOrchardServices os)
            : base(os)
        {
        }

        public override EventTypes Type
        {
            get { return EventTypes.user_view_card; }
        }

        protected override void Execute(ViewCardWeChatEventArgs model)
        {
            //throw new System.NotImplementedException();
        }
    }
}
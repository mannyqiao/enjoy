
namespace Enjoy.Core.Services
{
    using Orchard;
    using Enjoy.Core.EnjoyModels;
    using Enjoy.Core.WeChatModels;

    public class UpdateMemberCardWeChatMsgBehavior : WeChatMsgBehavior<UpdateMemberCardWeChatEventArgs>
    {
        public UpdateMemberCardWeChatMsgBehavior(IOrchardServices os) : base(os)
        {
        }

        public override EventTypes Type
        {
            get { return EventTypes.update_member_card; }
        }

        protected override void Execute(UpdateMemberCardWeChatEventArgs model)
        {
            
        }
    }
}
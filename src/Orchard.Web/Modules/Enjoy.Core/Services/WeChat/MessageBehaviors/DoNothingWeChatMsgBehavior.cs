
namespace Enjoy.Core.Services
{
    using Orchard;
    using Enjoy.Core.Models;
    public class DoNothingWeChatMsgBehavior : WeChatMsgBehavior<DoNothingWeChatMsgModel>
    {
        public DoNothingWeChatMsgBehavior(IOrchardServices os) : base(os)
        {
        }

        public override EventTypes Type
        {
            get
            {
                return EventTypes.Nothing;
            }

        }

        protected override void Execute(DoNothingWeChatMsgModel model)
        {

        }
    }
}
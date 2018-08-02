
namespace Enjoy.Core.Services
{
    using Orchard;
    using Enjoy.Core.EnjoyModels;
    using Enjoy.Core.WeChatModels;

    public class CardPayOrderWeChatMsgBehavior : WeChatMsgBehavior<PayOrderWeChatEventArgs>
    {
        public CardPayOrderWeChatMsgBehavior(IOrchardServices os)
            : base(os)
        {

        }

        public override EventTypes Type
        {
            get { return EventTypes.card_pay_order; }
        }

        protected override void Execute(PayOrderWeChatEventArgs model)
        {
            
        }
    }
}
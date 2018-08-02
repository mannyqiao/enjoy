
namespace Enjoy.Core.Services
{
    using Enjoy.Core.EnjoyModels;
    using Enjoy.Core.WeChatModels;
    using Orchard;
    /// <summary>
    /// 卡券审核通过消息
    /// </summary>
    public class AuditPassWeChatMsgBehavior : WeChatMsgBehavior<CardCouponAuditkWeChatEventArgs>
    {
        private readonly ICardCouponService CardCoupon;
        public AuditPassWeChatMsgBehavior(
            IOrchardServices os,
            ICardCouponService cardcoupon)
            : base(os)
        {
            this.CardCoupon = cardcoupon;
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
            this.CardCoupon.UpdateStatus(model.CardId, CCStatus.Approved, model.RefuseReason);
        }
    }
}
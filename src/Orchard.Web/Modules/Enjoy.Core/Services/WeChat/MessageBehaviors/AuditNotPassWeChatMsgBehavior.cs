﻿


namespace Enjoy.Core.Services
{
    
    using Enjoy.Core.WeChatModels;
    using Orchard;
    /// <summary>
    /// 卡券审核不通过消息
    /// </summary>
    public class AuditNotPassWeChatMsgBehavior : WeChatMsgBehavior<CardCouponAuditkWeChatEventArgs>
    {
        private readonly ICardCouponService CardCoupon;
        public AuditNotPassWeChatMsgBehavior(IOrchardServices os, ICardCouponService cardcoupon)
            : base(os)
        {
            this.CardCoupon = cardcoupon;
        }
        public override EventTypes Type
        {
            get
            {
                return EventTypes.card_not_pass_check;
            }
        }

        protected override void Execute(CardCouponAuditkWeChatEventArgs model)
        {
            this.CardCoupon.UpdateStatus(model.CardId, CardCouponStates.Rejected, model.RefuseReason);
        }
    }
}
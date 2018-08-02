using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Services
{
    using Enjoy.Core.EnjoyModels;
    using Enjoy.Core.WeChatModels;
    using Orchard;
    /// <summary>
    /// 库存预警消息通知
    /// </summary>
    public class CardSkuRemindWeChatMsgBehavior : WeChatMsgBehavior<SkuRemindWeChatEventArgs>
    {
        private readonly ICardCouponService CardCoupon;
        private readonly IOrchardServices OS;
        public CardSkuRemindWeChatMsgBehavior(
            IOrchardServices os,
            ICardCouponService cardCoupon) : base(os)
        {
            this.OS = os;
            this.CardCoupon = cardCoupon;
        }

        public override EventTypes Type
        {
            get { return EventTypes.card_sku_remind; }
        }

        protected override void Execute(SkuRemindWeChatEventArgs model)
        {
            //var card = this.CardCoupon.GetCardCounpon(model.CardId);            
            //card.Merchant.EnjoyUser.Mobile
        }
    }
}
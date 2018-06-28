using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Services
{
    using Enjoy.Core.Models;
    using Orchard;
    public class CardSkuRemindWeChatMsgBehavior : WeChatMsgBehavior<SkuRemindWeChatEventArgs>
    {
        public CardSkuRemindWeChatMsgBehavior(IOrchardServices os) : base(os)
        {
        }

        public override EventTypes Type
        {
            get { return EventTypes.card_sku_remind; }
        }

        protected override void Execute(SkuRemindWeChatEventArgs model)
        {
            
        }
    }
}
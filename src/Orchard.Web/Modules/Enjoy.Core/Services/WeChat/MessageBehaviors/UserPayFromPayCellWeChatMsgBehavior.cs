using Orchard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Services
{
    using Orchard;
    using Enjoy.Core.EnjoyModels;
    using Enjoy.Core.WeChatModels;

    public class UserPayFromPayCellWeChatMsgBehavior : WeChatMsgBehavior<PayFromPayCellWeChatEventArgs>
    {
        public UserPayFromPayCellWeChatMsgBehavior(IOrchardServices os)
            : base(os)
        {
        }

        public override EventTypes Type
        {
            get
            {
                return EventTypes.user_pay_from_pay_cell;
            }
        }

        protected override void Execute(PayFromPayCellWeChatEventArgs model)
        {
            //throw new System.NotImplementedException();
        }
    }
}
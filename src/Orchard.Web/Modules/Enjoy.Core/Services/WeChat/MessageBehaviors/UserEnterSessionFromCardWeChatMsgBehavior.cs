using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Services
{
    using Enjoy.Core.Models;
    using Orchard;
    public class UserEnterSessionFromCardWeChatMsgBehavior : WeChatMsgBehavior<EnterSessinoFromCardWeChatEventArgs>
    {
        public UserEnterSessionFromCardWeChatMsgBehavior(IOrchardServices os) : base(os)
        {
        }

        public override EventTypes Type
        {
            get { return EventTypes.user_enter_session_from_card; }

        }

        protected override void Execute(EnterSessinoFromCardWeChatEventArgs model)
        {
            throw new NotImplementedException();
        }
    }
}
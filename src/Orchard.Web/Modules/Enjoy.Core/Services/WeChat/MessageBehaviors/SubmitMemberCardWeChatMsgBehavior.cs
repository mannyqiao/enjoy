using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Services
{
    using Orchard;
    using Enjoy.Core.Models;
    public class SubmitMemberCardWeChatMsgBehavior : WeChatMsgBehavior<SubmitMemberCardWeChatEventArgs>
    {
        public SubmitMemberCardWeChatMsgBehavior(IOrchardServices os) : base(os)
        {

        }
        public override EventTypes Type
        {
            get { return EventTypes.submit_membercard_user_info; }
        }

        protected override void Execute(SubmitMemberCardWeChatEventArgs model)
        {

        }
    }
}
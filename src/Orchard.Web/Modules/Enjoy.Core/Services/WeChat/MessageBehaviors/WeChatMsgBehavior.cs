using System;
using Enjoy.Core.Models;

namespace Enjoy.Core.Services
{
    using Orchard;
    using System;
    using Enjoy.Core.Models;
    using System.Xml;
    using System.Xml.Serialization;
    using System.IO;

    public abstract class WeChatMsgBehavior<T> : IWeChatEventBehavior
        where T : WeChatMsgModel
    {
        protected readonly IOrchardServices OS;
        public WeChatMsgBehavior(IOrchardServices os)
        {
            this.OS = os;
        }
        public abstract EventTypes Type { get; }


        public void Execute(object model)
        {
            this.Execute(model as T);
        }
        protected abstract void Execute(T model);
    }
}
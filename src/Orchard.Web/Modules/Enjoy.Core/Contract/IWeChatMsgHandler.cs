﻿

namespace Enjoy.Core
{
    using Orchard;
    public interface IWeChatMsgHandler : IDependency
    {
        void Handle(IWxMsgToken token);
        void Handle(string xmlMsg);
    }
}

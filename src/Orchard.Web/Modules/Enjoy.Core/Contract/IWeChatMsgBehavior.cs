
namespace Enjoy.Core
{
    using Orchard;
    using Enjoy.Core.Models;
    using System;
    public interface IWeChatMsgBehavior : IDependency
    {
        ActionResponse<WeChatMsgModel> Execute(IWxMsgToken token, Func<string, WeChatMsgModel> resolve = null);
    }
}

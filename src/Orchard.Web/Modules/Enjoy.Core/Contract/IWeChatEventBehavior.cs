
namespace Enjoy.Core
{
    using Orchard;
    using Enjoy.Core.Models;
    using System;
    public interface IWeChatEventBehavior : IDependency
    {
        EventTypes Type { get; }
        void Execute(object model);
    }
}

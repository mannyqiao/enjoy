
namespace Enjoy.Core
{
    using Orchard;
    public interface IWeChatEventBehavior : IDependency
    {
        EventTypes Type { get; }
        void Execute(object model);
    }
}

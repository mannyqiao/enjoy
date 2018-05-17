

namespace Enjoy.Core
{
    using Orchard;
    using Enjoy.Core.Models;
    public interface IWeChatApi : IDependency
    {
        string GetToken(string appid, string appsecret);
        ApplyProtocolWxResponse GetApplyProtocol();
    }
}


namespace Enjoy.Core
{
    using Orchard;
    using Enjoy.Core.Models;
    public interface IWeChatApi : IDependency
    {
        string GetToken(string appid, string appsecret);
        string GetToken();
        ApplyProtocolWxResponse GetApplyProtocol();

        UploadMediaWxResponse UploadMaterial(string name,byte[] buffers);
    }
}
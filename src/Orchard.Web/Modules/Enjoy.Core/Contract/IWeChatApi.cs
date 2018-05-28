

namespace Enjoy.Core
{
    using Orchard;
    using Enjoy.Core.Models;
    using Enjoy.Core.Models.Records;

    public interface IWeChatApi : IDependency
    {
        string GetToken(string appid, string appsecret);
        string GetToken();
        ApplyProtocolWxResponse GetApplyProtocol();

        UploadMediaWxResponse UploadMaterial(string name,byte[] buffers);
        UploadMediaWxResponse UploadMaterialToCDN(byte[] buffers);

        WapperWxResponse<CreateSubmerchantResponse> CreateSubmerchant(WapperWxRequest<SubMerchant> submerchant);
    }
}
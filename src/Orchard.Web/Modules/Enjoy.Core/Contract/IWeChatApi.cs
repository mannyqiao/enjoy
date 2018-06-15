

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

        WxResponseWapper<CreateSubmerchantResponse> CreateSubmerchant(WxRequestWapper<SubMerchant> submerchant);

        WxSession CreateWxSession(IWxLoginUser loginUseer);

        IWxAuthorization GetWxAuth(IWxLoginUser loginUser);

        string GetOpenId(IWxLoginUser loginUser);

        IWxAccessToken GetWxAccessToken(string appid, string secret);
    }
}
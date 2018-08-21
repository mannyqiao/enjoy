

namespace Enjoy.Core
{
    using Orchard;
    using Enjoy.Core.EnjoyModels;
    using Enjoy.Core.Records;
    using Enjoy.Core.WeChatModels;

    public interface IWeChatApi : IDependency
    {
        string GetToken(string appid, string appsecret);

        string GetToken();

        ApplyProtocolWxResponse GetApplyProtocol();

        UploadMediaWxResponse UploadMaterial(string name, byte[] buffers);

        UploadMediaWxResponse UploadMaterialToCDN(byte[] buffers);

        WxResponseWapper<CreateSubmerchantResponse> CreateSubmerchant(WxRequestWapper<SubMerchant> submerchant);

        WxSession CreateWxSession(IWxLoginUser loginUseer);

        IWxAuthorization GetWxAuth(IWxLoginUser loginUser);

        string GetOpenId(IWxLoginUser loginUser);

        WeChatModels.WxUser GetWxUser(string openid);

        IWxAccessToken GetWxAccessToken(string appid, string secret);

        void CheckCardAgentQulification();

        /// <summary>
        /// 创建子商户门店
        /// </summary>
        /// <param name="model"></param>
        //void CreateMerchantShop(ShopModel model);        
    }
}
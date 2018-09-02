﻿

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

        WxSession CreateWxSession(IWxAuthContext loginUseer);

        IWxAuthorization GetWxAuth(IWxAuthContext loginUser);

        string GetOpenId(IWxAuthContext loginUser);

        WeChatModels.WeChatUserInfo GetWxUser(string openid);

        IWxAccessToken GetWxAccessToken(string appid, string secret);

        void CheckCardAgentQulification();
        WeChatUserInfo Decrypt(string encryptedData, string iv, string sessionKey);
        IWxAuthorization GetSessionKey(string code, string appid, string secret);

        WxAccessToken GetAccessTokenByCode(string code);

        NormalWxResponse DeleteCardCoupon(string cardid);

        QueryCardCouponWxResponse QueryCardCouponOnWechat();
        void SetMemberCardFieldIfActiveByWx(string cardid);
        /// <summary>
        /// 创建统一下单相关参数
        /// </summary>
        /// <param name="jsApiPay"></param>
        /// <returns></returns>
        string JsPay(JsApiPay jsApiPay);
        //WeChatUserH5Auth GetWeChatUserH5(string openid);
        /// <summary>
        /// 创建子商户门店
        /// </summary>
        /// <param name="model"></param>
        //void CreateMerchantShop(ShopModel model);        
    }
}
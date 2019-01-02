



namespace Enjoy.Core
{
    using Enjoy.Core.Records;
    using Enjoy.Core.WeChatModels;
    using System;

    public static class WeChatUserInfoExtension
    {
        public static WxUser CreateWxUser(this WeChatUserInfo userInfo, RegistryTypes type, string appid)
        {
            return new WxUser()
            {
                AppId = appid,
                AvatarUrl = userInfo.AvatarUrl,
                City = userInfo.City,
                Country = userInfo.Country,
                CreatedTime = DateTime.Now.ToUnixStampDateTime(),
                LastActivityTime = DateTime.Now.ToUnixStampDateTime(),
                Mobile = string.Empty,
                NickName = userInfo.NickName,
                OpenId = userInfo.OpenId,
                Province = userInfo.Province,
                RegistryType = RegistryTypes.Miniprogram,
                UnionId = userInfo.UnionId
            };
        }
    }
}
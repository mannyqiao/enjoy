

namespace Enjoy.Core
{
    using Enjoy.Core.EnjoyModels;
    using Orchard;
    using EnjoyModels = Enjoy.Core.EnjoyModels;
    using WeChatModels = Enjoy.Core.WeChatModels;
    using Records = Enjoy.Core.Records;
    using System;
    public interface IWxUserService : IDependency
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        long Register(EnjoyModels::WxUserModel userModel);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        EnjoyModels::WxUserModel Register(WeChatModels::WeChatUserInfo userModel);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        EnjoyModels::WxUserModel GetWxUser(long id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unionid"></param>
        /// <returns></returns>
        
        EnjoyModels::WxUserModel GetWxUser(string unionid);

        Records::WxUser GetWxUser(string appid, string openid);

    }
}
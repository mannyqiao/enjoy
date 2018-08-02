

namespace Enjoy.Core
{
    using Orchard;
    using EnjoyModels = Enjoy.Core.EnjoyModels;
    using WeChatModels = Enjoy.Core.WeChatModels;
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
        long Register(WeChatModels::WxUser userModel);
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
    }
}


namespace Enjoy.Core
{
    using Orchard;
    using Enjoy.Core.Models;
    public interface IWxUserService : IDependency
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        long Register(WxUserModel userModel);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        long Register(WxUser userModel);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        WxUserModel GetWxUser(long id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unionid"></param>
        /// <returns></returns>
        WxUserModel GetWxUser(string unionid);
    }
}
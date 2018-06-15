

namespace Enjoy.Core
{
    using System;
    using WeChat.Models;
    public interface IWxLoginUser
    {
        /// <summary>
        /// 登录Code
        /// </summary>
        string Code { get; }


        string IV { get; }
        /// <summary>
        /// 加密数据
        /// </summary>
        string Data { get; }
        /// <summary>
        /// 签名
        /// </summary>
        string Signature { get; }
    }
}

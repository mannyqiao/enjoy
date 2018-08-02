using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core
{
    public enum VerifyTypes
    {
        /// <summary>
        /// 注册验证码
        /// </summary>
        SignUp = 1,
        /// <summary>
        /// 找回密码
        /// </summary>
        FindPassword = 2,
        /// <summary>
        /// 提现验证码
        /// </summary>
        ExtractToBank = 3,
    }
}
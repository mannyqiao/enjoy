using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core
{
    public enum VATypes
    {
        /// <summary>
        /// 会员卡余额
        /// </summary>
        BalanceOfMermberCard = 1,
        /// <summary>
        /// 鼓励金余额
        /// </summary>
        BalanceOfRaward = 2,
    }

    public enum VAStates
    {
        Normal = 1,
        Abnormity = 2,
    }
}
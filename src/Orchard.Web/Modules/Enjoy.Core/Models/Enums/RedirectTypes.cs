using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core
{
    public enum RedirectTypes
    {
        /// <summary>
        /// 支付
        /// </summary>
        Pay = 1,
        /// <summary>
        /// 储值
        /// </summary>
        Prepaid = 2,
        /// <summary>
        /// 查看余额
        /// </summary>
        Balance = 3,
        /// <summary>
        /// 查看交易记录
        /// </summary>
        Transaction = 4,
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core
{
    /// <summary>
    /// 交易类型
    /// </summary>
    public enum TradeTypes
    {
        /// <summary>
        /// 充值
        /// </summary>
        Recharge = 1,
        /// <summary>
        /// 消费
        /// </summary>
        Consume = 2,
        /// <summary>
        /// 用户红包
        /// </summary>
        Redbag = 3,
    }
    public enum TradeStates
    {
        Waiting = 1,
        Success = 2,
        Timeout = 3,
        Cancel = 4,

    }
}
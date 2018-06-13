using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core
{
    /// <summary>
    /// 卡券状态
    /// </summary>
    [Flags]
    public enum CCStatus
    {
        /// <summary>
        /// 编辑中
        /// </summary>
        Editing = 0,
        /// <summary>
        /// 已发布
        /// </summary>
        Published = 1,
        /// <summary>
        /// 已过期
        /// </summary>
        Expired = 2,

        /// <summary>
        /// 领用完毕
        /// </summary>
        RunOut = 4,
        /// <summary>
        /// 使用完毕
        /// </summary>
        UseCompleted = 8,

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Models
{
    public enum MerchantStatus
    {
        /// <summary>
        /// 审核中
        /// </summary>
        CHECKING = 0,
        /// <summary>
        /// 已通过
        /// </summary>
        APPROVED = 1,
        /// <summary>
        /// 已驳回
        /// </summary>
        REJECTED = 2,
        /// <summary>
        /// 协议过期
        /// </summary>
        EXPIRED = 3
    }
}
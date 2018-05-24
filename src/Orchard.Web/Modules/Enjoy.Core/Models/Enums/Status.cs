﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core
{
    public enum MerchantStatus
    {
        NotFond = 0,

        NotSubmit = 1,
        /// <summary>
        /// 审核中
        /// </summary>
        CHECKING = 2,
        /// <summary>
        /// 已通过
        /// </summary>
        APPROVED = 3,
        /// <summary>
        /// 已驳回
        /// </summary>
        REJECTED = 4,
        /// <summary>
        /// 协议过期
        /// </summary>
        EXPIRED = 5
    }
}
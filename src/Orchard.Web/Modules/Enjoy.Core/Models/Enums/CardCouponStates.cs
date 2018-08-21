using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core
{
    /// <summary>
    /// 卡券状态
    /// </summary>
    
    public enum CardCouponStates
    {
        /// <summary>
        /// 编辑中
        /// </summary>
        Editing = 1,
        /// <summary>
        /// 审核中
        /// </summary>
        Checking = 2,
        /// <summary>
        /// 
        /// </summary>
        Approved = 3,
        /// <summary>
        /// 
        /// </summary>
        Rejected = 4,        
        /// <summary>
        /// 已过期
        /// </summary>
        Expired = 5,
        /// <summary>
        /// 领用完毕
        /// </summary>
        RunOut = 6,        
        /// <summary>
        /// 发布出错
        /// </summary>
        PublishedError = 7,
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core
{
    public static class AuditStatusExtension
    {
        public static string WithDisplayName(this AuditStatus status)
        {
            switch (status)
            {
                case AuditStatus.NotFond:
                    return "未创建";
                case AuditStatus.UnCommitted:
                    return "未提交审核";
                case AuditStatus.CHECKING:
                    return "审核中";
                case AuditStatus.EXPIRED:
                    return "已过期";
                case AuditStatus.REJECTED:
                    return "已拒绝";
                case AuditStatus.APPROVED:
                    return "审核通过";
            }
            return "未定义的状态";
        }
    }
}
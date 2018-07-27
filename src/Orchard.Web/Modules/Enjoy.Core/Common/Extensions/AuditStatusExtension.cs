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
                    return "协议过期";
                case AuditStatus.REJECTED:
                    return "已拒绝";
                case AuditStatus.APPROVED:
                    return "审核通过";
            }
            return "未定义的状态";
        }
        public static string GenerateActionLink(this AuditStatus status, long id)
        {
            switch (status)
            {
                case AuditStatus.UnCommitted:
                    return string.Concat(
                        string.Format("<a class='btn btn-outline  btn-primary  btn-xs' id='btn_audit' data-id='{0}' href = '/merchant/audit'>提交审核</a>", id),
                        string.Format("<a class='btn btn-outline  btn-primary  btn-xs' href = '/merchant/view?id={0}'>修改</a>", id)
                        );
                case AuditStatus.REJECTED:
                    return string.Format("<a class='btn btn-outline  btn-primary  btn-xs' href = '/merchant/view?id={0}'>修改</a>", id);
                case AuditStatus.EXPIRED:
                    return string.Format("<a  class='btn btn-outline  btn-primary  btn-xs' href = '/merchant/view?id={0}'>延长协议</a>", id);
                case AuditStatus.CHECKING:
                case AuditStatus.APPROVED:
                    return string.Format("<a  class='btn btn-outline  btn-primary  btn-xs' href = '/merchant/view?id={0}'>查看</a>", id);
                default:
                    return string.Empty;
            }
        }
    }
}


using System;

namespace Enjoy.Core.EnjoyModels
{
    public class QCloudSMS : ISMSEntity
    {


        public QCloudSMS(string mobile, SMSNotifyTypes type, params string[] parameters)
        {
            this.Mobile = mobile;
            this.Parameters = parameters;
            this.CreatedTime = DateTime.UtcNow.ToUnixStampDateTime();
            this.Type = type;
        }
        public string Mobile { get; private set; }


        public long CreatedTime { get; private set; }

        public SMSNotifyTypes Type { get; private set; }
        public string[] Parameters
        {
            get;
            private set;
        }
        public bool Equals(ISMSEntity other)
        {
            //如果同一手机号码在5分钟以内 则认为这两条 短信是相同的,相同的短信不允许重发
            return this.Mobile.Equals(other.Mobile)
                && Math.Abs(this.CreatedTime - other.CreatedTime) < 60 * 5;
        }

        public string GetBody()
        {
            switch (this.Type)
            {
                case SMSNotifyTypes.VerifyCode:
                    return string.Format("你本次操作的验证码是{0}，有效期{1}分钟。", this.Parameters);
                case SMSNotifyTypes.MerchantAudit:
                    return string.Format("你的商户[{0}],审核{1}. 登录 https://www.yourc.club/ 查看详情", this.Parameters);
            }
            return string.Empty;
        }
    }
}
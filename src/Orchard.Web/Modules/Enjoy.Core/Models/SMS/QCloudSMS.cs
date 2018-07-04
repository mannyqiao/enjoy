

using System;

namespace Enjoy.Core.Models
{
    public class QCloudSMS : ISMSEntity
    {

        public QCloudSMS(string mobile, VerifyTypes type, string verifyCode)
        {
            this.Mobile = mobile;
            this.VerifyCode = verifyCode;
            this.Type = type;
            this.CreatedTime = DateTime.Now.ToUnixStampDateTime();
        }
        public string Mobile { get; private set; }

        public string VerifyCode { get; private set; }
        public long CreatedTime { get; private set; }

        public VerifyTypes Type { get; private set; }

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
                case VerifyTypes.SignUp:
                    return string.Format("尊敬的用户,你本次操作的验证码是{0}. 两分钟内有效.", this.VerifyCode);
                case VerifyTypes.FindPassword:
                    return string.Format("[{0}]你正在找回密码!你的验证码为[{1}]. 5分钟内有效.如果不是本人操作请联系你的服务商.", EnjoyConstant.PlatformShortName, this.VerifyCode);
                case VerifyTypes.ExtractToBank:
                    return string.Format("[{0}]你正在进提现!你的验证码为[{1}]. 5分钟内有效.如果不是本人操作请联系你的服务商.", EnjoyConstant.PlatformShortName, this.VerifyCode);
            }
            return string.Empty;
        }
    }
}
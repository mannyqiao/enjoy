﻿

namespace Enjoy.Core.EnjoyModels
{
    using Enjoy.Core.Records;

    public class EnjoyUserModel : IEnjoyUser
    {
        public EnjoyUserModel(EnjoyUser record)
        {            
            this.Id = record.Id;
            this.Mobile = record.Mobile;
            this.NickName = record.NickName;
            this.Password = record.Password;
            this.WxUser = new WxUserModel(record.WxUser);
            this.LastPassword = record.LastPassword;
            this.CreatedTime = record.CreatedTime;
            this.LastActiveTime = record.LastActivityTime;
            this.AvatarUrl = record.Profile;
        }
        public EnjoyUserModel() { }
        public long Id { get; set; }
        public string Mobile { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
        public WxUserModel WxUser { get; set; }
        public string LastPassword { get; set; }
        public long CreatedTime { get; set; }
        public long LastActiveTime { get; set; }
        public string AvatarUrl { get; set; }
    }
}
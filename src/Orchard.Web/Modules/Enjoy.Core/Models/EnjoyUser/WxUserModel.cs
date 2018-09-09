

namespace Enjoy.Core.EnjoyModels
{
    using System;

    public class WxUserModel : IModelKey<long>
    {
        public WxUserModel() { }
        public WxUserModel(Records.WxUser user)
        {
            if (user != null)
            {
                this.Id = user.Id;
                this.CreatedTime = user.CreatedTime;
                this.City = user.City;
                this.Country = user.Country;
                this.LastActiveTime = user.LastActivityTime;
                this.Mobile = user.Mobile;
                this.NickName = user.NickName;
                //this.OpenId = user.OpenId;
                this.Province = user.Province;
                this.UnionId = user.UnionId;
                this.RegistryType = user.RegistryType;
                this.AvatarUrl = user.AvatarUrl;
            }

        }
        public WxUserModel(WeChatModels.WeChatUserInfo user)
        {
            if (user != null)
            {
                this.UnionId = user.UnionId;
                this.City = user.City;
                this.Country = user.Country;
                this.CreatedTime = DateTime.UtcNow.ToUnixStampDateTime();
                this.LastActiveTime = DateTime.UtcNow.ToUnixStampDateTime();
                this.NickName = user.NickName;
                //this.OpenId = user.OpenId;
                this.Province = user.Province;
                this.UnionId = user.UnionId;
                this.RegistryType = RegistryTypes.Miniprogram;
            }
        }
        public long Id { get; set; }
        public string UnionId { get; set; }
        //public string OpenId { get; set; }
        public string Mobile { get; set; }
        public string NickName { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string AvatarUrl { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public long CreatedTime { get; set; }
        public long? LastActiveTime { get; set; }
        public RegistryTypes RegistryType { get; set; }

        
    }
}
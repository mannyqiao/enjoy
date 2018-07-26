

namespace Enjoy.Core.Models
{
    
    public class WxUserModel
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
                this.LastActiveTime = user.LastActiveTime;
                this.Merchant = new MerchantModel(user.Merchant);
                this.Mobile = user.Mobile;
                this.NickName = user.NickName;
                this.OpenId = user.OpenId;
                this.OwnApp = user.OwnApp;
                this.Province = user.Province;
                this.Subscribe = user.Subscribe;
                this.SubscribeTime = user.SubscribeTime;
                this.UnionId = user.UnionId;
            }

        }
        public  int Id { get; set; }

        public MerchantModel Merchant { get; set; }

        public  string UnionId { get; set; }
        public  string OpenId { get; set; }
        public  string Mobile { get; set; }
        public  string NickName { get; set; }
        public  string Country { get; set; }
        public  string Province { get; set; }
        public  string City { get; set; }
        public  bool Subscribe { get; set; }
        public  string OwnApp { get; set; }
        public  long? SubscribeTime { get; set; }
        public  long CreatedTime { get; set; }
        public  long? LastActiveTime { get; set; }
    }
}
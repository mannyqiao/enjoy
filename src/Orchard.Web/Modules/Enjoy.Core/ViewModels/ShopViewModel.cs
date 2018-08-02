

namespace Enjoy.Core.ViewModels
{
    using Enjoy.Core.Records;
    using Enjoy.Core.EnjoyModels;
    using Newtonsoft.Json;
    using Enjoy.Core.WeChatModels;

    public class ShopViewModel
    {
        public ShopViewModel(ShopModel model)
        {
            this.Id = model.Id;
            this.Merchant = model.Merchant.BrandName;
            this.ShopName = model.ShopName;
            this.Address = new AddressViewModel(model.Address);
            this.Coordinate = model.Coordinate;
            this.Leader = model.Leader;
            this.MerchantId = model.Merchant.Id;

        }
        public ShopViewModel(string merchant)
        {
            this.Merchant = merchant;
        }
        public ShopViewModel() { }
        public long Id { get; set; }
        public string Merchant { get; set; }
        public string ShopName { get; set; }
        public string Leader { get; set; }
        public long MerchantId { get; set; }
        //public string Address { get; set; }
        public string Coordinate { get; set; }
        public string AddressInfo
        {
            get
            {
                return this.Address == null
                    ? string.Empty
                    : string.Join("/", new string[] {
                        this.Address.Province,
                        this.Address.City,
                        this.Address.Area
                    });

            }
        }
        [JsonIgnore]
        public AddressViewModel Address { get; set; }
        [JsonIgnore]
        public ApplyProtocolWxResponse Protocol { get; set; }

    }
}
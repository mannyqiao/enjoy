

namespace Enjoy.Core.ViewModels
{
    using Enjoy.Core.Models.Records;
    using Enjoy.Core.Models;
    public class ShopViewModel
    {
        public ShopViewModel(ShopModel model)
        {
            this.Id = model.Id;
            this.Merchant = model.Merchant;
            this.ShopName = model.ShopName;
            this.Address = new AddressViewModel(model.Address);
            this.Coordinate = model.Coordinate;
            this.Leader = model.Leader;
        }
        public ShopViewModel(MerchantModel merchant)
        {
            this.Merchant = merchant;
        }
        public int Id { get; set; }
        public MerchantModel Merchant { get; set; }
        public string ShopName { get; set; }
        public string Leader { get; set; }
        //public string Address { get; set; }
        public string Coordinate { get; set; }
        public AddressViewModel Address { get; set; }
        public ApplyProtocolWxResponse Protocol { get; set; }
    }
}
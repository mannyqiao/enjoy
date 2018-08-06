

namespace Enjoy.Core.EnjoyModels
{
    using Enjoy.Core.Records;
    using Enjoy.Core.ViewModels;

    public class ShopModel : IModelKey<long>
    {
        public ShopModel(Shop shop)
        {
            this.ShopName = shop.ShopName;
            this.Merchant = new MerchantModel(shop.Merchant);
            this.Id = shop.Id;
            this.Leader = shop.Leader;
            this.Address = shop.Address;
            this.Coordinate = shop.Coordinate;
        }
        public ShopModel(ShopViewModel viewModel)
        {
            this.ShopName = viewModel.ShopModel.ShopName;
            this.Merchant = viewModel.ShopModel.Merchant;  //new MerchantModel() { Id = viewModel.ShopModel.Merchant.Id };
            this.Id = viewModel.ShopModel.Id;
            this.Leader = viewModel.ShopModel.Leader;
            this.Address = viewModel.AddressInfo;
            this.Coordinate = string.IsNullOrEmpty(viewModel.ShopModel.Coordinate) ? "{}" : viewModel.ShopModel.Coordinate;
        }
        public ShopModel(MerchantModel merchant)
        {
            this.Merchant = merchant;
        }
        public long Id { get; set; }
        public MerchantModel Merchant { get; set; }
        public string ShopName { get; set; }
        public string Leader { get; set; }
        public string Address { get; set; }
        public string Coordinate { get; set; }


    }
}
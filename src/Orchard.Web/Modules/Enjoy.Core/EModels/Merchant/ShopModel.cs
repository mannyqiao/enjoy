

namespace Enjoy.Core.EModels
{
    using Enjoy.Core.ViewModels;
    
    public class ShopModel : IModelKey<long>
    {
        public ShopModel(Records.Shop shop)
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
            this.ShopName = viewModel.ShopName;
            this.Merchant = new MerchantModel() { Id = viewModel.MerchantId };
            this.Id = viewModel.Id;
            this.Leader = viewModel.Leader;
            this.Address = viewModel.AddressInfo;
            this.Coordinate = viewModel.Coordinate ?? "{}";
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
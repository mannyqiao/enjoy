

namespace Enjoy.Core.Models
{
    using Records = Enjoy.Core.Models.Records;
    public class ShopModel : IEntityKey<int>
    {
        public ShopModel(Records::Shop shop)
        {
            this.ShopName = shop.ShopName;
            this.Merchant = new MerchantModel(shop.Merchant);
            this.Id = shop.Id;
            this.Leader = shop.Leader;
            this.Address = shop.Address;
            this.Coordinate = shop.Coordinate;
        }
        public ShopModel(MerchantModel merchant)
        {
            this.Merchant = merchant;
        }
        public int Id { get; set; }
        public MerchantModel Merchant { get; set; }
        public string ShopName { get; set; }
        public string Leader { get; set; }
        public string Address { get; set; }
        public string Coordinate { get; set; }
    }
}
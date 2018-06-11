

namespace Enjoy.Core.Models
{
    using Records = Enjoy.Core.Models.Records;
    public class ShopModel : IEntityKey<int>
    {
        public ShopModel(Records::Shop shop)
        {
            this.ShopName = shop.ShopName;
            this.Merchant = shop.Merchant;
            this.Id = shop.Id;
            this.Leader = shop.Leader;
            this.Address = shop.Address;
            this.Coordinate = shop.Coordinate;            
        }
        public int Id { get; set; }
        public Records::Merchant Merchant { get; set; }
        public string ShopName { get; set; }
        public string Leader { get; set; }
        public string Address { get; set; }
        public string Coordinate { get; set; }
    }
}

namespace Enjoy.Core.Models
{
    using Enjoy.Core.Models;
    using WeChat.Models;
    using Records = Enjoy.Core.Models.Records;
    public class CardCounponModel : IEntityKey<int>
    {
        public CardCounponModel(Records::CardCoupon record)
        {
            this.Id = record.Id;
            this.Merchant = new MerchantModel(record.Merchant);
            this.Quantity = record.Quantity;
            this.Type = record.Type;
            this.WxNo = record.WxNo;
            this.BrandName = record.BrandName;
            this.LastUpdateTime = record.LastUpdateTime;
            this.CreatedTime = record.CreatedTime;
            this.CardCouponWapper = record.JsonMetadata.DeserializeSpecificCardCoupon(record.Type);


        }
        public CardCounponModel() { }
        public int Id { get; set; }
        public string BrandName { get; set; }
        public MerchantModel Merchant { get; set; }

        public CardTypes Type { get; set; }

        public int Quantity { get; set; }

        public string WxNo { get; set; }

        public long CreatedTime { get; set; }
        public long LastUpdateTime { get; set; }
        public WxCardCouponWapper<ICardCoupon> CardCouponWapper { get; set; }
    }
}

namespace Enjoy.Core.EnjoyModels
{
    
    using Orchard.Data;
    using Enjoy.Core.WeChatModels;
    using Enjoy.Core.Records;

    //[DoNotMap]
    public class CardCounponModel : IModelKey<long>
    {
        public CardCounponModel(CardCoupon record)
        {
            this.Id = record.Id;
            this.Merchant = new MerchantModel(record.Merchant);
            this.Quantity = record.Quantity;
            this.Type = record.Type;
            this.WxNo = record.WxNo;
            this.BrandName = record.BrandName;
            this.LastUpdateTime = record.LastActivityTime;
            this.CreatedTime = record.CreatedTime;
            this.CardCouponWapper = record.JsonMetadata.DeserializeSpecificCardCoupon(record.Type);
            this.State = record.Status;
            this.ErrMsg = record.ErrMsg;
        }
        public CardCounponModel() { }
        public long Id { get; set; }
        public string BrandName { get; set; }
        public MerchantModel Merchant { get; set; }

        public CardTypes Type { get; set; }

        public int Quantity { get; set; }

        public string WxNo { get; set; }

        public long CreatedTime { get; set; }
        public long LastUpdateTime { get; set; }
        public CardCouponStates State { get; set; }
        public string ErrMsg { get; set; }
        public WxCardCouponWapper<ICardCoupon> CardCouponWapper { get; set; }
    }
}
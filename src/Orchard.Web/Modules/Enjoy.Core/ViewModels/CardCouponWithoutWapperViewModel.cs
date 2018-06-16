
namespace Enjoy.Core.ViewModels
{
    using Models = Enjoy.Core.Models;
    using System;
    public class CardCouponWithoutWapperViewModel
    {
        public CardCouponWithoutWapperViewModel(Models::CardCounponModel model)
        {
            this.Id = model.Id;
            this.BrandName = model.BrandName;
            this.MerchantName = model.Merchant.BrandName;
            this.CreatedTime = model.CreatedTime.ToDateTimeFromUnixStamp();
            this.LastUpdateTime = model.CreatedTime.ToDateTimeFromUnixStamp();
            this.Quantity = model.Quantity;
            this.TypeName = model.Type.TextOf();
            this.WxNo = model.WxNo;
            this.Type = model.Type;
        }

        public int Id { get; set; }
        public string BrandName { get; set; }
        public string MerchantName { get; set; }
        public CardTypes Type { get; set; }
        public string TypeName { get; set; }

        public int Quantity { get; set; }

        public string WxNo { get; set; }

        public DateTime? CreatedTime { get; set; }
        public DateTime? LastUpdateTime { get; set; }
        public CCStatus Status { get; set; }
    }
}
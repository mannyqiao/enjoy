
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
            this.Merchant = model.Merchant.BrandName;
            this.CreatedTime = model.CreatedTime.ToDateTimeFromUnixStamp().ToString("yyyy-MM-dd HH:mm");
            this.LastUpdateTime = model.LastUpdateTime.ToDateTimeFromUnixStamp().ToString("yyyy-MM-dd HH:mm");
            this.Quantity = model.Quantity;
            this.Type = model.Type.TextOf();
            this.WxNo = model.WxNo;
            this.Status = model.Status.TextOf();
            this.CardType = model.Type;
        }

        public int Id { get; set; }
        public string BrandName { get; set; }
        public string Merchant { get; set; }

        public string Type { get; set; }
        public CardTypes CardType { get; set; }

        public int Quantity { get; set; }

        public string WxNo { get; set; }

        public string CreatedTime { get; set; }
        public string LastUpdateTime { get; set; }
        public string Status { get; set; }
    }
}
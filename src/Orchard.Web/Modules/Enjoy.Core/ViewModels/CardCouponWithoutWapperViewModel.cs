
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
            this.CreatedTime = model.CreatedTime.ToDateTimeString();
            this.LastUpdateTime = model.LastUpdateTime.ToDateTimeString();
            this.Quantity = model.Quantity;
            this.Type = model.Type.TextOf();
            this.WxNo = model.WxNo;
            this.Status = model.Status.TextOf();
            this.CardType = model.Type;
            //this.DelAble = !((model.Status & CCStatus.Published) == CCStatus.Published);
        }

        public long Id { get; set; }
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
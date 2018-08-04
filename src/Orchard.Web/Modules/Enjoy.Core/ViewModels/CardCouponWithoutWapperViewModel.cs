
namespace Enjoy.Core.ViewModels
{
    using Enjoy.Core.EnjoyModels;
    using System;
    public class CardCouponWithoutWapperViewModel
    {
        public CardCouponWithoutWapperViewModel(CardCounponModel model)
        {
            this.Id = model.Id;
            this.BrandName = model.BrandName;
            this.Merchant = model.Merchant.BrandName;
            this.CreatedTime = model.CreatedTime.ToDateTimeString();
            this.LastUpdateTime = model.LastUpdateTime.ToDateTimeString();
            this.Quantity = model.Quantity;
            this.Type = model.Type.TextOf();
            this.WxNo = model.WxNo;
            this.StateWithName = model.State.WithDisplayName();
            this.CardType = model.Type;
            this.State = model.State;
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
        public CardCouponStates State { get; set; }
        public string StateWithName { get; set; }

    }
}
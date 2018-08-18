

namespace Enjoy.Core.ViewModels
{
    using Enjoy.Core.EnjoyModels;
    public class MerchantCardCouponViewModel
    {
        public MerchantCardCouponViewModel() { }
        public MerchantCardCouponViewModel(MerchantModel model)
        {
            this.MerchantId = model.Id;
            this.WxMerchantId = model.MerchantId ?? 0;
            this.BrandName = model.BrandName;
        }
        public long MerchantId { get; set; }
        public long WxMerchantId { get; set; }
        public string BrandName { get; set; }
        public CardTypes CardType { get; set; }

    }
}
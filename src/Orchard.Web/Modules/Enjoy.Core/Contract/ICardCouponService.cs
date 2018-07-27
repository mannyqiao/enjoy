

namespace Enjoy.Core
{
    using Enjoy.Core.Models;
    using Enjoy.Core.ViewModels;
    using Orchard;
    using Enjoy.Core.Models.Records;
    public interface ICardCouponService : IDependency
    {
        ActionResponse<CardCounponModel> SaveOrUpdate(CardCounponModel model);
        PagingData<CardCounponModel> QueryCardCoupon(PagingCondition condition, CardTypes type);
        PagingData<CardCounponModel> QueryCardCoupon(QueryFilter filter, PagingCondition condition);
        CardCounponModel GetCardCounpon(int id);
        CardCounponModel GetCardCounpon(string cardid);
        NormalWxResponse TestwhiteList(string[] wechatids);
        QRCodeWxResponse CreateQRCode(string cardid);
        WxUserCardCoupon QueryWxUserCardCoupon(string userCardCode);
        
        void SaveWxUserCardCoupon(WxUserCardCouponModel model);
        
        CreateCouponWxResponse Publish(int id);
        void UpdateStatus(string wxno, CCStatus status, string reson);
    }
}

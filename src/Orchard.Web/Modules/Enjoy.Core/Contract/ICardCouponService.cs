

namespace Enjoy.Core
{
    using Orchard;
    using Enjoy.Core.EnjoyModels;
    using Enjoy.Core.ViewModels;
    using Enjoy.Core.WeChatModels;
    using Enjoy.Core.Records;    
    
    public interface ICardCouponService : IDependency
    {
        ActionResponse<CardCounponModel> SaveOrUpdate(CardCounponModel model);
        PagingData<CardCounponModel> QueryCardCoupon(PagingCondition condition, CardTypes type);
        PagingData<CardCounponModel> QueryCardCoupon(QueryFilter filter, PagingCondition condition);
        PagingData<CardCouponNearby> QueryCardCoupon(Location location, PagingCondition condition, float distance);
        CardCounponModel GetCardCounpon(long id);
        CardCounponModel GetCardCounpon(string cardid);
        NormalWxResponse TestwhiteList(string[] wechatids);
        QRCodeWxResponse CreateQRCode(string cardid);
        WxUserCardCoupon QueryWxUserCardCoupon(string userCardCode);
        
        void SaveWxUserCardCoupon(WxUserCardCouponModel model);
        
        CreateCouponWxResponse Publish(long id);
        void UpdateStatus(string wxno, CardCouponStates status, string reson);
        BaseResponse DeleteById(long id);
        
        
    }
}

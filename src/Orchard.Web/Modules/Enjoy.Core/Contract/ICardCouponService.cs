

namespace Enjoy.Core
{
    using Orchard;
    using Enjoy.Core.EnjoyModels;
    using Enjoy.Core.ViewModels;
    using Enjoy.Core.WeChatModels;
    using Enjoy.Core.Records;
    using System.Collections.Generic;

    public interface ICardCouponService : IDependency
    {
        ActionResponse<CardCounponModel> SaveOrUpdate(CardCounponModel model);
        PagingData<CardCounponModel> QueryCardCoupon(PagingCondition condition, CardTypes type);
        PagingData<CardCounponModel> QueryCardCoupon(WebQueryFilter filter, PagingCondition condition);
        IList<CardCounponModel> QueryCardCoupon(long merchantId,CardTypes[] types, CardCouponStates[] states);
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

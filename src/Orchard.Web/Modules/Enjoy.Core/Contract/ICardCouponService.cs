

namespace Enjoy.Core
{
    using Enjoy.Core.Models;
    using Enjoy.Core.ViewModels;
    using Orchard;

    public interface ICardCouponService : IDependency
    {
        //WxCardCouponWapper<ICardCoupon> CreateCardCouponInstance(CardTypes type);

        ActionResponse<CardCounponModel> SaveOrUpdate(CardCounponModel model);

        PagingData<CardCounponModel> QueryCardCoupon(PagingCondition condition, CardTypes type);

        PagingData<CardCounponModel> QueryCardCoupon(QueryFilter filter, PagingCondition condition);

        CardCounponModel GetCardCounpon(int id);

        NormalWxResponse TestwhiteList(string[] wechatids);

        QRCodeWxResponse CreateQRCode(string cardid);

        CreateCouponWxResponse Publish(int id);

    }
}

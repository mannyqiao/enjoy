

namespace Enjoy.Core
{
    using Enjoy.Core.Models;
    using Orchard;

    public interface ICardCouponService : IDependency
    {
        //WxCardCouponWapper<ICardCoupon> CreateCardCouponInstance(CardTypes type);

        ActionResponse<CardCounponModel> SaveOrUpdate(CardCounponModel model);

        PagingData<CardCounponModel> QueryCardCounpon(PagingCondition condition, CardTypes type);

        CardCounponModel GetCardCounpon(int id);

        NormalWxResponse TestwhiteList(string[] wechatids);

        QRCodeWxResponse CreateQRCode(string cardid);
        CreateCouponWxResponse Publish(int id);

    }
}

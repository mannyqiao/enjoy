

namespace Enjoy.Core
{
    using Enjoy.Core.Models;
    using Orchard;

    public interface ICardCouponService : IDependency
    {
        WxCardCouponWapper<ICardCoupon> CreateCardCouponInstance(CardTypes type);

        ActionResponse<CardCounponModel> SaveOrUpdate(CardCounponModel model);

        PagingData<CardCounponModel> QueryCardCounpon(int page, CardTypes type);

        CardCounponModel GetCardCounpon(int id);

    }
}

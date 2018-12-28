

namespace Enjoy.Core
{

    using Orchard;
    using Enjoy.Core.EnjoyModels;
    using Enjoy.Core.Records;
    public interface IShopService : IDependency
    {
        PagingData<ShopModel> QueryMyShops(long merchantid, PagingCondition paging);

        ShopModel GetDefaultShop(long shopid);

        PagingData<ShopModel> QueryShops(WebQueryFilter filter, PagingCondition paging);

        void SaveOrUpdate(ShopModel model);

        void DeleteShop(long id);

    }
}

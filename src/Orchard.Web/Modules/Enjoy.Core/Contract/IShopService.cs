

namespace Enjoy.Core
{
    using Enjoy.Core.ViewModels;
    using Orchard;
    using Models = Enjoy.Core.Models;
    using Records = Enjoy.Core.Models.Records;
    public interface IShopService : IDependency
    {
        Models::PagingData<Models::ShopModel> QueryMyShops(long merchantid, PagingCondition paging);

        Models::ShopModel GetDefaultShop(long shopid);

        Models::PagingData<Models::ShopModel> QueryShops(QueryFilter filter, PagingCondition paging);

        void SaveOrUpdate(Models::ShopModel model);

        void DeleteShop(long id);

    }
}

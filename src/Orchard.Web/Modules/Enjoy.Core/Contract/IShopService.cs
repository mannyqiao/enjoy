

namespace Enjoy.Core
{
    using Enjoy.Core.ViewModels;
    using Orchard;
    using Models = Enjoy.Core.Models;
    using Records = Enjoy.Core.Models.Records;
    public interface IShopService : IDependency
    {
        Models::PagingData<Models::ShopModel> QueryMyShops(int merchantid, PagingCondition paging);

        Models::ShopModel GetDefaultShop(int shopid);

        Models::PagingData<Models::ShopModel> QueryShops(QueryFilter filter, PagingCondition paging);

    }
}

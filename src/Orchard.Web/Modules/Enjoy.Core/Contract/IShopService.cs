

namespace Enjoy.Core
{
    using Orchard;
    using Enjoy.Core.Models;
    using Records = Enjoy.Core.Models.Records;
    public interface IShopService : IDependency
    {
        PagingData<ShopModel> QueryShops(int merchantid, int page);

        PagingData<ShopModel> QueryShops(int page);        
    }
}

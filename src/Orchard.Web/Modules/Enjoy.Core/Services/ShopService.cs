
namespace Enjoy.Core.Services
{
    using Records = Enjoy.Core.Models.Records;
    using Models = Enjoy.Core.Models;
    using Orchard;
    using NHibernate;
    using System;
    using NHibernate.Criterion;

    public class ShopService : QueryBaseService<Records::Shop, Models::ShopModel>, IShopService
    {
        public ShopService(IOrchardServices os)
            : base(os)
        {

        }
        public override string ModelTypeName
        {
            get
            {
                return typeof(Models::ShopModel).Name;
            }
        }
        public Models.PagingData<Models.ShopModel> QueryShops(int merchantid, int page)
        {
            return base.Query(page, builder =>
            {
                builder.Add(Expression.Eq("Merchant_Id", merchantid));

            }, (record) => new Models.ShopModel(record));
        }

        public Models.PagingData<Models.ShopModel> QueryShops(int page)
        {
            return base.Query(page, builder => { }, record => new Models.ShopModel(record));
        }

     
    }
}

namespace Enjoy.Core.Services
{
    using Records = Enjoy.Core.Models.Records;
    using Models = Enjoy.Core.Models;
    using Orchard;
    using NHibernate;
    using System;
    using NHibernate.Criterion;
    using Enjoy.Core.ViewModels;
    using System.Collections.Generic;

    public class ShopService : QueryBaseService<Records::Shop, Models::ShopModel>, IShopService
    {
        public ShopService(IOrchardServices os)
            : base(os)
        {

        }
        public override Type ModelType
        {
            get
            {
                return typeof(Models::ShopModel);
            }
        }

        public Models.ShopModel GetDefaultShop(int shopid)
        {
            return this.QueryFirstOrDefaut((builder) =>
            {
                builder.Add(Expression.Eq("Id", shopid));
            },
            r => new Models.ShopModel(r));
        }

        public Models.PagingData<Models.ShopModel> QueryMyShops(int merchantid, PagingCondition condition)
        {
            return base.Query(condition, builder =>
            {
                builder.Add(Expression.Eq("Merchant.Id", merchantid));

            }, (record) => new Models.ShopModel(record));
        }

        public Models.PagingData<Models.ShopModel> QueryShops(PagingCondition condition)
        {
            return base.Query(condition, builder =>
            {

            },
            record => new Models.ShopModel(record));
        }

        public Models.PagingData<Models.ShopModel> QueryShops(QueryFilter filter, PagingCondition condition)
        {
            return base.Query(condition, builder =>
            {
                foreach (var criteria in this.Criterias(filter))
                    builder.Add(criteria);



                foreach (var order in this.Orders(filter))
                    builder.AddOrder(order);
            },
            record => new Models.ShopModel(record));
        }
        public override IEnumerable<ICriterion> Criterias(QueryFilter filter)
        {
            
            if (string.IsNullOrEmpty(filter.Search.Value == null
                ? ""
                : filter.Search.Value.ToString()
                ) == false)
            {
                yield return Expression.Like("ShopName", filter.Search.Value) as ICriterion;
            }
            foreach (var criteria in base.Criterias(filter))
            {
                yield return criteria;
            }
        }
    }
}
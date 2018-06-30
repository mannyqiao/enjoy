
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
    using System.Linq;
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
            return this.QueryFirstOrDefault((builder) =>
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
                {
                    builder.Add(criteria);
                }

                foreach (var order in this.Orders(filter))
                {
                    builder.AddOrder(order);
                }

            },
            record => new Models.ShopModel(record));
        }
        public override IEnumerable<ICriterion> Criterias(QueryFilter filter)
        {
            var names = filter.Search.Value as string[];
            if (names != null && names.Count(o => !string.IsNullOrWhiteSpace(o)) > 0)
            {
                foreach (var name in names)
                {
                    yield return Expression.Like("ShopName", name) as ICriterion;
                }
            }

            foreach (var criteria in base.Criterias(filter))
            {
                yield return criteria;
            }
        }

        public void SaveOrUpdate(Models.ShopModel model)
        {
            this.SaveOrUpdate(model, Validate, Convert);

        }
        private Records::Shop Convert(Models::ShopModel model)
        {
            var record = this.ConvertToRecord<Int32>(model, (r, m) =>
            {

                if (r == null) r = new Records::Shop();
                r.Address = m.Address;
                r.ShopName = m.ShopName;
                r.Merchant = new Records.Merchant() { Id = m.Merchant.Id };
                r.Leader = m.Leader;
                r.Coordinate = m.Coordinate;

                return r;
            });
            return record;
        }
        public IResponse Validate(Models::ShopModel model)
        {
            var errors = new List<string>();
            if (string.IsNullOrEmpty(model.ShopName) || model.ShopName.Length > 20)
            {
                errors.Add("门店名称不能为空且不能大于20个字符");
            }
            return Models::VerifyResponse.CreateSuccessInstance();
        }

        public void DeleteShop(int id)
        {
            this.Delete(id);
        }
    }
}

namespace Enjoy.Core.Services
{
    using Enjoy.Core.Records;
    using Enjoy.Core.EnjoyModels;
    using Orchard;
    using NHibernate;
    using System;
    using NHibernate.Criterion;
    using Enjoy.Core.ViewModels;
    using System.Collections.Generic;
    using System.Linq;
    public class ShopService : QueryBaseService<Shop, ShopModel>, IShopService
    {
        public ShopService(IOrchardServices os)
            : base(os)
        {

        }
        public override Type ModelType
        {
            get
            {
                return typeof(ShopModel);
            }
        }

        public ShopModel GetDefaultShop(long shopid)
        {
            return this.QueryFirstOrDefault((builder) =>
            {
                builder.Add(Expression.Eq("Id", shopid));
            },
            r => new ShopModel(r));
        }

        public PagingData<ShopModel> QueryMyShops(long merchantid, PagingCondition condition)
        {
            return base.Query(condition, builder =>
            {
                builder.Add(Expression.Eq("Merchant.Id", merchantid));

            }, (record) => new ShopModel(record));
        }

        public PagingData<ShopModel> QueryShops(PagingCondition condition)
        {
            return base.Query(condition, builder =>
            {

            },
            record => new ShopModel(record));
        }

        public PagingData<ShopModel> QueryShops(QueryFilter filter, PagingCondition condition)
        {
            return Query(filter, condition, null, r => new ShopModel(r));            
        }


        public void SaveOrUpdate(ShopModel model)
        {
            this.SaveOrUpdate(model, Validate, RecordSetter);

        }
        protected override void RecordSetter(Shop record, ShopModel model)
        {
            record.Address = model.Address;
            record.ShopName = model.ShopName;
            record.Merchant = new Records.Merchant() { Id = model.Merchant.Id };
            record.Leader = model.Leader;
            record.Latitude = model.Latitude;
            record.Longitude = model.Longitude;
        }

        public IResponse Validate(ShopModel model)
        {
            var errors = new List<string>();
            if (string.IsNullOrEmpty(model.ShopName) || model.ShopName.Length > 20)
            {
                errors.Add("门店名称不能为空且不能大于20个字符");
            }
            return VerifyResponse.CreateSuccessInstance();
        }

        public void DeleteShop(long id)
        {
            this.Delete(id);
        }
    }
}


namespace Enjoy.Core.Services
{
    using Orchard;
    using Enjoy.Core;
    using System;
    using Enjoy.Core.Models;
    using Records = Enjoy.Core.Models.Records;
    using NHibernate;
    using NHibernate.Criterion;
    using System.Linq;
    using System.Reflection;
    using Enjoy.Core.ViewModels;
    using System.Collections.Generic;

    public abstract class QueryBaseService<R, M> : IQueryService<R, M>
        where R : class
        where M : class
    {
        protected readonly IOrchardServices OS;

        public abstract Type ModelType { get; }


        public QueryBaseService(IOrchardServices os)
        {
            this.OS = os;
        }

        public virtual PagingData<M> Query(Action<ICriteria> builder, Func<R, M> convert)
        {
            return Query(null, builder, convert);
        }

        public virtual PagingData<M> QueryByPaging(PagingCondition condition, Action<ICriteria> builder, Func<R, M> convert)
        {
            return Query(condition, builder, convert);
        }

        public PagingData<M> Query(PagingCondition condition, Action<ICriteria> builder, Func<R, M> convert)
        {
            var session = this.OS.TransactionManager.GetSession();
            var criteria = session.CreateCriteria(typeof(R));
            builder(criteria);

            var pageCriteria = CriteriaTransformer.Clone(criteria);

            criteria.ClearOrders();
            var page = (condition.Skip / condition.Take) - 1;
            return new PagingData<M>()
            {
                TotalCount = Convert.ToInt32(criteria.SetProjection(Projections.RowCount()).UniqueResult()),
                Items = pageCriteria.SetFirstResult(condition.Skip)
               .SetMaxResults(condition.Take)
               .List<R>()
               .Select(o => convert(o))
               .ToList(),
                Paging = new Paging(page, condition.Take)
            };
        }

        public virtual M QueryFirstOrDefaut(Action<ICriteria> builder, Func<R, M> convert)
        {
            var session = this.OS.TransactionManager.GetSession();
            var criteria = session.CreateCriteria(typeof(R));
            builder(criteria);


            return convert(criteria.SetFirstResult(0)
                .SetMaxResults(1)
                .UniqueResult<R>());
        }

        public ActionResponse<M> SaveOrUpdate(M model, Func<M, IResponse> validate, Func<M, R> convert)
        {
            var check = validate(model);
            if (check.HasError)
                return new ActionResponse<M>(check.ErrorCode);

            var session = this.OS.TransactionManager.GetSession();
            var record = convert(model);
            session.SaveOrUpdate(record);
            return new ActionResponse<M>(EnjoyConstant.Success, model);
        }

        public BaseResponse Delete(int id)
        {
            var session = this.OS.TransactionManager.GetSession();
            session.Delete(session.Get<R>(id));
            return new BaseResponse(EnjoyConstant.Success);
        }

        public BaseResponse Delete(ISQLQuery query)
        {
            return new BaseResponse(0);
        }

        public R GetRecord(int id)
        {
            var session = this.OS.TransactionManager.GetSession();
            return session.Get<R>(id);

        }

        public R ConvertToRecord<TKeyType>(M model, Func<R, M, R> convert)
        {
            var session = this.OS.TransactionManager.GetSession();
            var id = (model as IEntityKey<TKeyType>).Id;
            var record = session.Get<R>(id);//make sure record is query from NHibrate
            return convert(record, model);
        }

        public virtual IEnumerable<ICriterion> Criterias(QueryFilter filter)
        {
            foreach (var column in filter.Columns)
            {
                var type = column.Search.Value.PredictDbTypeBySearchColumeValue();
                if (column.Searchable == false || type == null)
                {
                    continue;
                }
                var values = column.Search.Value as string[];
                switch (type)
                {
                    case System.Data.DbType.DateTime:
                        yield return Expression.Eq(column.Data, DateTime.Parse(column.Search.Value.ToString()));
                        break;
                    case System.Data.DbType.Int32:
                        yield return Expression.Eq(column.Data, Int32.Parse(column.Search.Value.ToString()));
                        break;
                    case System.Data.DbType.Int64:
                        yield return Expression.Eq(column.Data, Int64.Parse(column.Search.Value.ToString()));
                        break;
                    case System.Data.DbType.Decimal:
                        yield return Expression.Eq(column.Data, decimal.Parse(column.Search.Value.ToString()));
                        break;
                    case System.Data.DbType.String:
                        yield return Expression.Eq(column.Data, column.Search.Value.ToString());
                        break;
                }
            }
        }

        public virtual IEnumerable<Order> Orders(QueryFilter filter)
        {
            return filter.Columns != null && filter.Columns.Any() && filter.Order != null && filter.Order.Any()
                    ? filter.Order.Select((order) =>
                    {
                        return new Order(filter.Columns[order.Column].Data, order.Dir == Direction.Asc);

                    }).ToList()
                    : new List<Order>();
        }
    }
}
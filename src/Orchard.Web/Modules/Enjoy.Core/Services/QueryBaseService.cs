

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
    public abstract class QueryBaseService<R, M> : IQueryService<R, M>
    {
        private readonly IOrchardServices OS;

        public abstract string ModelTypeName { get; }


        public QueryBaseService(IOrchardServices os)
        {
            this.OS = os;
        }

        public virtual PagingData<M> Query(Action<ICriteria> builder, Func<R, M> convert)
        {
            return Query(null, builder, convert);
        }

        public virtual PagingData<M> QueryByPaging(int page, Action<ICriteria> builder, Func<R, M> convert)
        {
            return Query(page, builder, convert);
        }

        public PagingData<M> Query(int? page, Action<ICriteria> builder, Func<R, M> convert)
        {
            var session = this.OS.TransactionManager.GetSession();
            var criteria = session.CreateCriteria(typeof(R));
            builder(criteria);

            var pageCriteria = CriteriaTransformer.Clone(criteria);
            var pagingc = PagingCondition.GenerateByPageAndSize(page, EnjoyConstant.DefaultPageSize);

            return new PagingData<M>()
            {
                TotalCount = Convert.ToInt32(criteria.SetProjection(Projections.RowCount()).UniqueResult()),
                Items = pageCriteria.SetFirstResult(pagingc.Skip)
               .SetMaxResults(pagingc.Take)
               .List<R>()
               .Select(o => convert(o))
               .ToList(),
                Paging = new Paging(page ?? 1)
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

        public DbWriteResponse<M> SaveOrUpdate(M model, Func<M, IResponse> validate, Func<M, R> convert)
        {
            var check = validate(model);
            if (check.HasError)
                return new DbWriteResponse<M>(check.ErrorCode);

            var session = this.OS.TransactionManager.GetSession();
            var record = convert(model);
            session.SaveOrUpdate(record);
            return new DbWriteResponse<M>(EnjoyConstant.Success, model);
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
    }
}
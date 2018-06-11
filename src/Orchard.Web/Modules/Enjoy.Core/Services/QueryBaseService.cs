

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
    }
}
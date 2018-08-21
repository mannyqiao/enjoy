

namespace Enjoy.Core.Services
{
    using Orchard;
    using Enjoy.Core;
    using System;
    using Enjoy.Core.EnjoyModels;
    using Enjoy.Core.Records;
    using NHibernate;
    using NHibernate.Criterion;
    using System.Linq;
    using System.Collections.Generic;

    public abstract class QueryBaseService<R, M> : IQueryService<R, M>
        where R : IEntityKey<long>
        where M : IModelKey<long>
    {
        protected readonly IOrchardServices OS;

        public abstract Type ModelType { get; }


        public QueryBaseService(IOrchardServices os)
        {
            this.OS = os;
        }

        public PagingData<M> Query(
            QueryFilter filter,
            PagingCondition condition,
            Action<ICriteria> builder,
            Func<R, M> convert)
        {
            var session = this.OS.TransactionManager.GetSession();
            var criteria = session.CreateCriteria(typeof(R));
            condition = condition ?? new PagingCondition(0, int.MaxValue);
            if (builder != null)
            {
                builder(criteria);
            }
            if (filter != null)
            {
                criteria.WithQueryFilter(filter).WithQueryOrder(filter);                
            }
            criteria.ClearOrders();
            var pageCriteria = CriteriaTransformer.Clone(criteria);
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

        public PagingData<M> Query(PagingCondition condition, Action<ICriteria> builder, Func<R, M> convert)
        {
            return Query(null, condition, builder, convert);
        }

        public PagingData<M> Query(Action<ICriteria> builder, Func<R, M> convert)
        {
            return Query(null, builder, convert);
        }

        public M QueryFirstOrDefault(Action<ICriteria> builder, Func<R, M> convert)
        {
            var result = Query(new PagingCondition(0, 1), builder, convert);
            return result.GetSigleOrDefault();
        }

        public ActionResponse<M> SaveOrUpdate(M model, Func<M, IResponse> validate, Action<R, M> setter)
        {
            var check = validate(model);
            if (check.HasError)
                return new ActionResponse<M>(check.ErrorCode);
            var session = this.OS.TransactionManager.GetSession();
            var record = session.Get<R>(model.Id);
            if (record == null)
                record = Activator.CreateInstance<R>();
            setter(record, model);
            session.SaveOrUpdate(record);
            model.Id = record.Id;
            return new ActionResponse<M>(EnjoyConstant.Success, model);
        }

        public BaseResponse Delete(long id)
        {
            var session = this.OS.TransactionManager.GetSession();
            session.Delete(session.Get<R>(id));
            return new BaseResponse(EnjoyConstant.Success);
        }



        public BaseResponse Delete(QueryFilter filter)
        {
            var session = this.OS.TransactionManager.GetSession();
            var criteria = session.CreateCriteria(typeof(R));
            criteria.WithQueryFilter(filter);
            foreach (var item in criteria.SetMaxResults(int.MaxValue).List<R>())
            {
                session.Delete(item);
            }
            return new BaseResponse(EnjoyConstant.Success);
        }

        protected abstract void RecordSetter(R record, M model);

        public Record QueryFirstOrDefault<Record>(Action<ICriteria> builder) where Record : IEntityKey<long>
        {
            if (builder == null) throw new NullReferenceException("builder can't be null.");
            var session = this.OS.TransactionManager.GetSession();
            var criteria = session.CreateCriteria(typeof(Record));
            return criteria.SetMaxResults(1).UniqueResult<Record>();
        }





        //public virtual PagingData<M> Query(Action<ICriteria> builder, Func<R, M> convert)
        //{
        //    return Query(null, builder, convert);
        //}

        //public virtual PagingData<M> Query(PagingCondition condition, Action<ICriteria> builder, Func<R, M> convert)
        //{
        //    return Query(condition, builder, convert);
        //}

        //public PagingData<M> Query(PagingCondition condition, Action<ICriteria> builder, Func<R, M> convert)
        //{
        //    var session = this.OS.TransactionManager.GetSession();
        //    var criteria = session.CreateCriteria(typeof(R));
        //    builder(criteria);

        //    var pageCriteria = CriteriaTransformer.Clone(criteria);

        //    criteria.ClearOrders();
        //    var page = (condition.Skip / condition.Take) - 1;
        //    return new PagingData<M>()
        //    {
        //        TotalCount = Convert.ToInt32(criteria.SetProjection(Projections.RowCount()).UniqueResult()),
        //        Items = pageCriteria.SetFirstResult(condition.Skip)
        //       .SetMaxResults(condition.Take)
        //       .List<R>()
        //       .Select(o => convert(o))
        //       .ToList(),
        //        Paging = new Paging(page, condition.Take)
        //    };
        //}


        //public virtual M QueryFirstOrDefault(Action<ICriteria> builder, Func<R, M> convert)
        //{
        //    var session = this.OS.TransactionManager.GetSession();
        //    var criteria = session.CreateCriteria(typeof(R));
        //    builder(criteria);
        //    return convert(criteria.SetFirstResult(0)
        //        .SetMaxResults(1)
        //        .UniqueResult<R>());
        //}
        //public virtual T QueryFirstOrDefault<T>(Action<ICriteria> builder)
        //{
        //    var session = this.OS.TransactionManager.GetSession();
        //    var criteria = session.CreateCriteria(typeof(R));
        //    builder(criteria);
        //    return criteria.SetFirstResult(0)
        //        .SetMaxResults(1)
        //        .UniqueResult<T>();
        //}
        //public ActionResponse<M> SaveOrUpdate(
        //    M model,
        //    Func<M, IResponse> validate,
        //    Action<R, M> setter)
        //{
        //    var check = validate(model);
        //    if (check.HasError)
        //        return new ActionResponse<M>(check.ErrorCode);
        //    var session = this.OS.TransactionManager.GetSession();
        //    var record = session.Get<R>(model.Key);
        //    if (record == null)
        //        record = Activator.CreateInstance<R>();
        //    setter(record, model);
        //    session.SaveOrUpdate(record);
        //    model.Key = record.Id;
        //    return new ActionResponse<M>(EnjoyConstant.Success, model);
        //}
        //protected abstract void RecordSetter(R record, M model);
        //public BaseResponse Delete(long id)
        //{
        //    var session = this.OS.TransactionManager.GetSession();
        //    session.Delete(session.Get<R>(id));
        //    return new BaseResponse(EnjoyConstant.Success);
        //}

        //public BaseResponse Delete(ISQLQuery query)
        //{
        //    return new BaseResponse(0);
        //}

        //public R GetRecord(int id)
        //{
        //    var session = this.OS.TransactionManager.GetSession();
        //    return session.Get<R>(id);
        //}

        //public R ConvertToRecord<TKeyType>(M model, Func<R, M, R> convert)
        //{
        //    var session = this.OS.TransactionManager.GetSession();
        //    var id = (model as IEntityKey<TKeyType>).Id;
        //    var record = session.Get<R>(id);//make sure record is query from NHibrate
        //    return convert(record, model);
        //}

    }
}
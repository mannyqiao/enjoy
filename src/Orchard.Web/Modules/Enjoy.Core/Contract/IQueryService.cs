
namespace Enjoy.Core
{
    using Enjoy.Core.EModels;
    using Enjoy.Core.ViewModels;
    using NHibernate;
    using Orchard;
    using System;
    public interface IQueryService<TRecord, TModel> : IDependency, IQueryFilterBuilder
        where TRecord : IEntityKey<long>
        where TModel : IModelKey<long>
    {
        PagingData<TModel> Query(QueryFilter filter, PagingCondition condition, Action<ICriteria> builder, Func<TRecord, TModel> convert);

        PagingData<TModel> Query(PagingCondition condition, Action<ICriteria> builder, Func<TRecord, TModel> convert);

        PagingData<TModel> Query(Action<ICriteria> builder, Func<TRecord, TModel> convert);

        TModel QueryFirstOrDefault(Action<ICriteria> builder, Func<TRecord, TModel> convert);

        Type ModelType { get; }

        ActionResponse<TModel> SaveOrUpdate(TModel model,Func<TModel, IResponse> validate,Action<TRecord, TModel> setter);

        BaseResponse Delete(long id);

        BaseResponse Delete(ISQLQuery query);

        BaseResponse Delete(QueryFilter filter);

    }
}
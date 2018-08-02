
namespace Enjoy.Core
{
    using Enjoy.Core.EnjoyModels;
    using Enjoy.Core.ViewModels;
    using NHibernate;
    using Orchard;
    using System;
    public interface IQueryService<TRecord, TModel> : IDependency
        where TRecord : IEntityKey<long>
        where TModel : IModelKey<long>
    {
        PagingData<TModel> Query(QueryFilter filter, PagingCondition condition, Action<ICriteria> builder, Func<TRecord, TModel> convert);

        PagingData<TModel> Query(PagingCondition condition, Action<ICriteria> builder, Func<TRecord, TModel> convert);

        PagingData<TModel> Query(Action<ICriteria> builder, Func<TRecord, TModel> convert);

        TModel QueryFirstOrDefault(Action<ICriteria> builder, Func<TRecord, TModel> convert);
        Record QueryFirstOrDefault<Record>(Action<ICriteria> builder) where Record : IEntityKey<long>;
        Type ModelType { get; }

        ActionResponse<TModel> SaveOrUpdate(TModel model, Func<TModel, IResponse> validate, Action<TRecord, TModel> setter);

        BaseResponse Delete(long id);



        BaseResponse Delete(QueryFilter filter);

    }
}
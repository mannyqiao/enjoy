
namespace Enjoy.Core
{
    using Enjoy.Core.Models;
    using NHibernate;
    using Orchard;
    using System;
    public interface IQueryService<TRecord, TModel> : IDependency
    {
        PagingData<TModel> QueryByPaging(int page,
            Action<ICriteria> builder,
            Func<TRecord, TModel> convert);

        PagingData<TModel> Query(Action<ICriteria> builder,
            Func<TRecord, TModel> convert);

        TModel QueryFirstOrDefaut(Action<ICriteria> builder,
            Func<TRecord, TModel> convert);

        string ModelTypeName { get; }

        DbWriteResponse<TModel> SaveOrUpdate(TModel model, Func<TModel, IResponse> validate, Func<TModel, TRecord> convert);

        BaseResponse Delete(int id);

        BaseResponse Delete(ISQLQuery query);
    }
}
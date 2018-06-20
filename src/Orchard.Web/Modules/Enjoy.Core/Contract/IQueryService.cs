
namespace Enjoy.Core
{
    using Enjoy.Core.Models;
    using NHibernate;
    using Orchard;
    using System;
    public interface IQueryService<TRecord, TModel> : IDependency, IQueryFilterBuilder
        where TRecord : class
        where TModel : class
    {
        PagingData<TModel> QueryByPaging(
            PagingCondition condition,
            Action<ICriteria> builder,
            Func<TRecord, TModel> convert);

        PagingData<TModel> Query(
            Action<ICriteria> builder,            
            Func<TRecord, TModel> convert);

        TModel QueryFirstOrDefaut(
            Action<ICriteria> builder,
            Func<TRecord, TModel> convert);

        Type ModelType { get; }

        ActionResponse<TModel> SaveOrUpdate(
            TModel model, 
            Func<TModel, IResponse> validate, 
            Func<TModel, TRecord> convert);

        BaseResponse Delete(int id);

        BaseResponse Delete(ISQLQuery query);

        TRecord ConvertToRecord<TKeyType>(
            TModel model, 
            Func<TRecord, TModel, TRecord> convert);

        
    }
}
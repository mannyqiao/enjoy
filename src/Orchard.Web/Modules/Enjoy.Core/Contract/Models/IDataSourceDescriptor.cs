using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core
{
    public interface IDataSourceDescriptor<TModel>
    {
        int ErrorCode { get; }
        string ErrorMessage { get; }
        IEnumerable<TModel> Records { get; }
        TModel GetSigleOrDefault();
        TModel GetSigleOrDefault(Func<TModel, bool> selector);
    }
}
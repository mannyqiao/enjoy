
namespace Enjoy.Core
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public interface IDataSourceDescriptor<TModel>
    {
        [JsonProperty("error_code")]
        int ErrorCode { get; }

        [JsonProperty("error_message")]
        string ErrorMessage { get; }

        [JsonProperty("data")]
        IEnumerable<TModel> Data { get; }
        TModel GetSigleOrDefault();
        TModel GetSigleOrDefault(Func<TModel, bool> selector);
    }
}
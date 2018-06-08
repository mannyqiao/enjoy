
namespace Enjoy.Core
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public interface IQueryResponseDescriptor<TModel> : IResponse
    {
        [JsonProperty("data")]
        IEnumerable<TModel> Items { get; }

        TModel GetSigleOrDefault();

        TModel GetSigleOrDefault(Func<TModel, bool> selector);
    }
}
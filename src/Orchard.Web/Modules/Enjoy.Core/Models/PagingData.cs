
namespace Enjoy.Core.EnjoyModels
{
    using Enjoy.Core;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    public class PagingData<E> : QueryResponseDescriptor<E>
    {
        public PagingData() :
            base(EnjoyConstant.Success)
        {

        }
        public PagingData(IEnumerable<E> items)
            : base(items)
        {

        }
        [JsonProperty("recordsTotal")]
        public int TotalCount { get; set; }

        [JsonProperty("recordsFiltered")]
        public int RecordsFiltered { get { return this.TotalCount; } }

        [JsonIgnore]
        public Paging Paging { get; set; }
    }
    public class Paging
    {
        public Paging(int page, int size)
        {
            this.Page = page;
            this.PageSize = size;
        }
        public Paging(int page)
            : this(page, EnjoyConstant.DefaultPageSize)
        {

        }
        public Paging() : this(1, EnjoyConstant.DefaultPageSize) { }
        [JsonProperty("size")]
        public int PageSize { get; set; }
        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("count")]
        public int? PageCount { get; set; }

    }
}
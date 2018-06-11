
namespace Enjoy.Core.Models
{
    using Enjoy.Core;
    using System.Collections.Generic;

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
        public int TotalCount { get; set; }
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
        public int PageSize { get; set; }
        public int Page { get; set; }
        public int PageCount { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core
{
    public class PagingCondition
    {
        public int Skip { get; set; }
        public int Take { get; set; }

        public PagingCondition(int skip, int take)
        {
            this.Skip = skip;
            this.Take = take;
        }


        public static PagingCondition GenerateByPageAndSize(int page, int pagesize)
        {
            return new PagingCondition((page - 1) * pagesize, pagesize);
        }
        public static PagingCondition GenerateByPageAndSize(int? page, int? pagesize)
        {
            if (page.HasValue == false)
            {
                return GenerateByPageAndSize(1, int.MaxValue);
            }
            else
            {
                if (pagesize.HasValue == false)
                {
                    pagesize = 20;
                }
                return GenerateByPageAndSize(page.Value, pagesize.Value);
            }
        }
    }
}
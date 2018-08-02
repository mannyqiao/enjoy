using Enjoy.Core.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.ViewModels
{
    public class ShopListViewModel
    {
        public IList<ShopViewModel> Items { get; set; }

        public PagingCondition Paging { get; set; }
    }
}
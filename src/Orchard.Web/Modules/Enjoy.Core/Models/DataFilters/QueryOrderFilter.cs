using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.EnjoyModels
{
    public class QueryOrderFilter
    {
        public string ColumnName { get; set; }
        public int Column { get; set; }
        public Direction Dir { get; set; }
    }
}

namespace Enjoy.Core.EnjoyModels
{
    using System.Collections.Generic;
    using System.Linq;
    using NHibernate;
    using NHibernate.Criterion;

    public class WebQueryFilter
    {
        public int Draw { get; set; }
        public int Start { get; set; }

        public int Length { get; set; }

        public List<QueryColumnFilter> Columns { get; set; }
        public List<QueryOrderFilter> Order { get; set; }
        public SearchColumnFilter Search { get; set; }
        public Dictionary<string, string> Fixation { get; set; }
        public string OrderBy
        {
            get
            {
                return Columns != null && Columns.Any() && Order != null && Order.Any()
                    ? Columns[Order[0].Column].Data
                    : string.Empty;
            }
        }

    }
}


namespace Enjoy.Core
{
    using Orchard;
    using System.Collections.Generic;        
    using NHibernate.Criterion;
    using Enjoy.Core.EnjoyModels;

    public interface IQueryFilterBuilder : IDependency
    {
        //IEnumerable<ICriterion> Criterias(QueryFilter filter);
        //IEnumerable<Order> Orders(QueryFilter filter);
    }
}

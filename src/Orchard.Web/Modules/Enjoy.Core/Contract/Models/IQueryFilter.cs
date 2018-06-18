

namespace Enjoy.Core
{
    using Orchard;
    using System.Collections.Generic;    
    using Enjoy.Core.ViewModels;
    using NHibernate.Criterion;

    public interface IQueryFilterBuilder : IDependency
    {
        IEnumerable<ICriterion> Criterias(QueryFilter filter);
        IEnumerable<Order> Orders(QueryFilter filter);
    }
}

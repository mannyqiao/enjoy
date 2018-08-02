

namespace Enjoy.Core
{
    using NHibernate;
    using Enjoy.Core.EModels;
    using System;
    using NHibernate.Criterion;
    using System.Linq;
    public static class ICriteriaExtension
    {
        public static ICriteria WithQueryFilter(this ICriteria criteria, QueryFilter filter)
        {
            if (criteria == null) throw new ArgumentNullException("criteria");
            if (filter == null) throw new ArgumentNullException("filter");
            if (filter.Columns == null) throw new ArgumentNullException("filter.Columns");

            foreach (var column in filter.Columns)
            {
                var type = column.Search.Value.PredictDbTypeBySearchColumeValue();
                if (column.Searchable == false || type == null)
                {
                    continue;
                }
                var values = column.Search.Value as string[];
                switch (type)
                {
                    case System.Data.DbType.DateTime:
                        for (int i = 0; i < values.Length; i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    //>=
                                    criteria.Add(Restrictions.Ge(column.Data, DateTime.Parse(values[i]).ToUnixStampDateTime()));
                                    break;
                                case 1:
                                    //<
                                    criteria.Add(Restrictions.Lt(column.Data, DateTime.Parse(values[i]).ToUnixStampDateTime()));
                                    break;
                                default:
                                    break;
                            }

                        }
                        break;
                    case System.Data.DbType.Int32:
                        criteria.Add(Restrictions.Eq(column.Data, Int32.Parse(values[0])));
                        break;
                    case System.Data.DbType.Int64:
                        criteria.Add(Restrictions.Eq(column.Data, Int64.Parse(values[0])));
                        break;
                    case System.Data.DbType.Decimal:
                        criteria.Add(Restrictions.Eq(column.Data, decimal.Parse(values[0])));
                        break;
                    case System.Data.DbType.String:
                        criteria.Add(Restrictions.Eq(column.Data, values[0]));
                        break;
                }
            }
            return criteria;
        }
        public static ICriteria WithQueryOrder(this ICriteria criteria, QueryFilter filter)
        {
            if (criteria == null) throw new ArgumentNullException("criteria");
            if (filter == null) throw new ArgumentNullException("filter");
            if (filter.Columns == null) throw new ArgumentNullException("filter.Columns");

            foreach (var order in filter.Order.Select((ctx) =>
            {
                return new Order(filter.Columns[ctx.Column].Data, ctx.Dir == Direction.Asc);
            }))
            {
                criteria.AddOrder(order);
            }
            return criteria;
        }
    }
}
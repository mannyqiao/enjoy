

namespace Enjoy.Core.Models
{
    using System;
    using System.Collections.Generic;
    using Enjoy.Core;
    using System.Linq;
    public abstract class DataDescriptor<T> : IDataSourceDescriptor<T>
    {
        public DataDescriptor(IEnumerable<T> records)
        {
            if (IsEmptyOrNullDataSource())
            {
                this.ErrorCode = EnjoyConstant.EmptyOrNullDataSource;
            }
        }
        public int ErrorCode { get; set; }

        public string ErrorMessage
        {
            get;
            protected set;
        }

        public abstract IEnumerable<T> Records { get; protected set; }

        public abstract T GetSigleOrDefault();
        public abstract T GetSigleOrDefault(Func<T, bool> selector);
        protected virtual bool IsEmptyOrNullDataSource()
        {
            return Records == null || Records.Count() == 0;
        }

    }
}
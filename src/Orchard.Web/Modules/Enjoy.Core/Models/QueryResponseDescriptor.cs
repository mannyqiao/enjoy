

namespace Enjoy.Core.Models
{
    using System;
    using System.Collections.Generic;
    using Enjoy.Core;
    using System.Linq;
    public abstract class QueryResponseDescriptor<T> : IQueryResponseDescriptor<T>
    {
        protected QueryResponseDescriptor() { }
        public QueryResponseDescriptor(IEnumerable<T> records)
        {
            this.Items = records;
            if (this.IsEmptyOrNullDataSource() == false)
            {
                this.ErrorCode = EnjoyConstant.Success;
                this.ErrorMessage = EnjoyConstant.ErrorrCodeDescriptor[this.ErrorCode];
            }
        }
        public QueryResponseDescriptor(int errorCode, string errorMessage, IEnumerable<T> records)
        {
            this.ErrorCode = errorCode;
            this.ErrorMessage = ErrorMessage;
            this.Items = records;
        }
        public QueryResponseDescriptor(int errorCode)
            : this(errorCode, EnjoyConstant.ErrorrCodeDescriptor[errorCode], Enumerable.Empty<T>())
        {

        }
        public QueryResponseDescriptor(int errorCode, T record)
            : this(errorCode, EnjoyConstant.ErrorrCodeDescriptor[errorCode], new List<T>() { record })
        {

        }
        public QueryResponseDescriptor(int errorCode, string errorMessage)
            : this(errorCode, errorMessage, Enumerable.Empty<T>())
        {

        }
        public QueryResponseDescriptor(int errorCode, IEnumerable<T> records)
            : this(errorCode, EnjoyConstant.ErrorrCodeDescriptor[errorCode], records)
        {

        }

        public int ErrorCode { get; protected set; }

        public string ErrorMessage
        {
            get;
            protected set;
        }

        public virtual IEnumerable<T> Items { get;  set; }

        public bool HasError
        {
            get
            {
                return !this.ErrorCode.Equals(EnjoyConstant.Success);
            }
        }

        public virtual T GetSigleOrDefault()
        {
            if (IsEmptyOrNullDataSource() == false)
            {
                return this.Items.FirstOrDefault();
            }
            return default(T);
        }
        public virtual T GetSigleOrDefault(Func<T, bool> selector)
        {
            if (IsEmptyOrNullDataSource() == false)
            {
                return this.Items.FirstOrDefault(selector);
            }
            return default(T);
        }
        protected virtual bool IsEmptyOrNullDataSource()
        {
            return Items == null || Items.Count() == 0;
        }

    }
}
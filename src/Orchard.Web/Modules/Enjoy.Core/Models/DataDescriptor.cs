

namespace Enjoy.Core.Models
{
    using System;
    using System.Collections.Generic;
    using Enjoy.Core;
    using System.Linq;
    public abstract class DataDescriptor<T> : IDataSourceDescriptor<T>
    {
        protected DataDescriptor() { }
        public DataDescriptor(IEnumerable<T> records)
        {
            this.Data = records;
            if (this.IsEmptyOrNullDataSource() == false)
            {
                this.ErrorCode = EnjoyConstant.Success;
                this.ErrorMessage = EnjoyConstant.ErrorrCodeDescriptor[this.ErrorCode];
            }
        }
        public DataDescriptor(int errorCode, string errorMessage, IEnumerable<T> records)
        {
            this.ErrorCode = errorCode;
            this.ErrorMessage = ErrorMessage;
            this.Data = records;
        }
        public DataDescriptor(int errorCode)
            : this(errorCode, EnjoyConstant.ErrorrCodeDescriptor[errorCode], Enumerable.Empty<T>())
        {

        }
        public DataDescriptor(int errorCode, T record)
            : this(errorCode, EnjoyConstant.ErrorrCodeDescriptor[errorCode], new List<T>() { record })
        {

        }
        public DataDescriptor(int errorCode, string errorMessage)
            : this(errorCode, errorMessage, Enumerable.Empty<T>())
        {

        }
        public DataDescriptor(int errorCode, IEnumerable<T> records)
            : this(errorCode, EnjoyConstant.ErrorrCodeDescriptor[errorCode], records)
        {

        }

        public int ErrorCode { get; protected set; }

        public string ErrorMessage
        {
            get;
            protected set;
        }

        public abstract IEnumerable<T> Data { get; protected set; }

        public virtual T GetSigleOrDefault()
        {
            if (IsEmptyOrNullDataSource() == false)
            {
                return this.Data.FirstOrDefault();
            }
            return default(T);
        }
        public virtual T GetSigleOrDefault(Func<T, bool> selector)
        {
            if (IsEmptyOrNullDataSource() == false)
            {
                return this.Data.FirstOrDefault(selector);
            }
            return default(T);
        }
        protected virtual bool IsEmptyOrNullDataSource()
        {
            return Data == null || Data.Count() == 0;
        }

    }
}
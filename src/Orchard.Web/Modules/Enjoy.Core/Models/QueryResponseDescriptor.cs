

namespace Enjoy.Core.EnjoyModels
{
    using System;
    using System.Collections.Generic;
    using Enjoy.Core;
    using System.Linq;
    using Newtonsoft.Json;
    public abstract class QueryResponseDescriptor<T> : IQueryResponseDescriptor<T>
    {

        protected QueryResponseDescriptor() { }
        public QueryResponseDescriptor(IEnumerable<T> records)
        {
            this.Items = records;
            if (this.IsEmptyOrNullDataSource() == false)
            {
                this.ErrorCode = Constants.Success;
                this.ErrorMessage = Constants.ErrorrCodeDescriptor[this.ErrorCode];
            }
        }
        public QueryResponseDescriptor(int errorCode, string errorMessage, IEnumerable<T> records)
        {
            this.ErrorCode = errorCode;
            this.ErrorMessage = ErrorMessage;
            this.Items = records;
        }
        public QueryResponseDescriptor(int errorCode)
            : this(errorCode, Constants.ErrorrCodeDescriptor[errorCode], Enumerable.Empty<T>())
        {

        }
        public QueryResponseDescriptor(int errorCode, T record)
            : this(errorCode, Constants.ErrorrCodeDescriptor[errorCode], new List<T>() { record })
        {

        }
        public QueryResponseDescriptor(int errorCode, string errorMessage)
            : this(errorCode, errorMessage, Enumerable.Empty<T>())
        {

        }
        public QueryResponseDescriptor(int errorCode, IEnumerable<T> records)
            : this(errorCode, Constants.ErrorrCodeDescriptor[errorCode], records)
        {

        }
        [JsonIgnore]
        public int ErrorCode { get; protected set; }

        [JsonIgnore]
        public string ErrorMessage
        {
            get;
            protected set;
        }
        [JsonProperty("draw")]
        public int Draw { get; set; }

        [JsonProperty("data")]
        public virtual IEnumerable<T> Items { get; set; }

        [JsonIgnore]
        public bool HasError
        {
            get
            {
                return !this.ErrorCode.Equals(Constants.Success);
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

namespace Enjoy.Core
{
    using System;
    using System.Collections.Generic;
    using Enjoy.Core.Models;
    using Enjoy.Core.Models.Records;
    using System.Linq;
    public class EnjoyUserProfile : DataDescriptor<EnjoyUser>
    {
        public EnjoyUserProfile(IEnumerable<EnjoyUser> records)
            : base(records)
        {
            this.Records = records;
        }
        public EnjoyUserProfile() : this(Enumerable.Empty<EnjoyUser>()) { }
        public override IEnumerable<EnjoyUser> Records { get; protected set; }

        public EnjoyUserProfile(int errorCode, string errorMessage)
            : this()
        {
            this.ErrorCode = errorCode;
            this.ErrorMessage = EnjoyConstant.ErrorrCodeDescriptor[this.ErrorCode];
            if (string.IsNullOrEmpty(this.ErrorMessage))
            {
                this.ErrorMessage = errorMessage;
            }
        }
        public EnjoyUserProfile(int errorCode) 
            : this(errorCode, EnjoyConstant.ErrorrCodeDescriptor[errorCode])
        {

        }
        public override EnjoyUser GetSigleOrDefault()
        {
            if (IsEmptyOrNullDataSource())
            {
                return Records.FirstOrDefault();
            }
            return null;
        }

        public override EnjoyUser GetSigleOrDefault(Func<EnjoyUser, bool> selector)
        {
            if (IsEmptyOrNullDataSource())
            {
                return Records.FirstOrDefault(selector);
            }
            return null;
        }
    }
}
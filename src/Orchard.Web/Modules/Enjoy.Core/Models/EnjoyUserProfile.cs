
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
        {
            this.Records = records;
            if (this.IsEmptyOrNullDataSource() == false)
            {
                this.ErrorCode = EnjoyConstant.Success;
                this.ErrorMessage = EnjoyConstant.ErrorrCodeDescriptor[this.ErrorCode];
            }
        }
        public EnjoyUserProfile(int errorCode, string errorMessage, IEnumerable<EnjoyUser> records)
        {
            this.ErrorCode = errorCode;
            this.ErrorMessage = ErrorMessage;
            this.Records = records;
        }
        public EnjoyUserProfile(int errorCode)
            : base(errorCode, EnjoyConstant.ErrorrCodeDescriptor[errorCode], Enumerable.Empty<EnjoyUser>())
        {

        }
        public EnjoyUserProfile(int errorCode, EnjoyUser record)
            : base(errorCode, EnjoyConstant.ErrorrCodeDescriptor[errorCode], new List<EnjoyUser>() { record })
        {

        }
        public EnjoyUserProfile(int errorCode, string errorMessage)
            : base(errorCode, errorMessage, Enumerable.Empty<EnjoyUser>())
        {

        }
        public EnjoyUserProfile(int errorCode, IEnumerable<EnjoyUser> records)
            : base(errorCode, EnjoyConstant.ErrorrCodeDescriptor[errorCode], records)
        {

        }


        public override IEnumerable<EnjoyUser> Records { get; protected set; }
      
        //public override EnjoyUser GetSigleOrDefault()
        //{
        //    if (IsEmptyOrNullDataSource() == false)
        //    {
        //        return Records.FirstOrDefault();
        //    }
        //    return null;
        //}

        //public override EnjoyUser GetSigleOrDefault(Func<EnjoyUser, bool> selector)
        //{
        //    if (IsEmptyOrNullDataSource())
        //    {
        //        return Records.FirstOrDefault(selector);
        //    }
        //    return null;
        //}
    }
}
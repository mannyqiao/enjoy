
namespace Enjoy.Core
{
    using System;
    using System.Collections.Generic;
    using Enjoy.Core.EnjoyModels;
    using Enjoy.Core.Records;
    using System.Linq;
    public class AuthQueryResponse : QueryResponseDescriptor<EnjoyUserModel>
    {

        public AuthQueryResponse(IEnumerable<EnjoyUserModel> records)
        {
            this.Items = records;
            if (this.IsEmptyOrNullDataSource() == false)
            {
                this.ErrorCode = Constants.Success;
                this.ErrorMessage = Constants.ErrorrCodeDescriptor[this.ErrorCode];
            }
        }
        public AuthQueryResponse(int errorCode, string errorMessage, IEnumerable<EnjoyUserModel> records)
        {
            this.ErrorCode = errorCode;
            this.ErrorMessage = ErrorMessage;
            this.Items = records;
        }
        public AuthQueryResponse(int errorCode)
            : base(errorCode, Constants.ErrorrCodeDescriptor[errorCode], Enumerable.Empty<EnjoyUserModel>())
        {

        }
        public AuthQueryResponse(int errorCode, EnjoyUserModel model)
            : base(errorCode, Constants.ErrorrCodeDescriptor[errorCode], new List<EnjoyUserModel>() { model })
        {

        }
        public AuthQueryResponse(int errorCode, string errorMessage)
            : base(errorCode, errorMessage, Enumerable.Empty<EnjoyUserModel>())
        {

        }
        public AuthQueryResponse(int errorCode, IEnumerable<EnjoyUserModel> records)
            : base(errorCode, Constants.ErrorrCodeDescriptor[errorCode], records)
        {

        }
    }
}
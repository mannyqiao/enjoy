

namespace Enjoy.Core.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System;
    public class ActionResponse<TModel> : BaseResponse
    {
        public ActionResponse(int error, TModel model)
            : base(error)
        {
            this.Model = model;
        }
        public ActionResponse(int error)
            : base(error)
        {
            this.Model = default(TModel);
        }
        public TModel Model { get; protected set; }
    }

    public class BaseResponse : IResponse
    {
        public BaseResponse(int error)
        {
            this.ErrorCode = error;
            this.ErrorMessage = EnjoyConstant.ErrorrCodeDescriptor[error];
        }
        public virtual int ErrorCode { get; protected set; }

        public virtual string ErrorMessage { get; protected set; }
        public bool HasError
        {
            get
            {
                return !this.ErrorCode.Equals(EnjoyConstant.Success);
            }
        }
    }

    public class VerifyResponse : ActionResponse<string[]>
    {
        public VerifyResponse(int error, string[] errors)
           : base(error, errors)
        {

        }
        private string errorMessage;
        public override string ErrorMessage
        {
            get
            {
                return string.Join("\r\n", this.Model);
            }
            protected set
            {
                errorMessage = value;
            }
        }
        public static VerifyResponse CreateSuccessInstance()
        {
            return new VerifyResponse(EnjoyConstant.Success, null);
        }
              
    }

}
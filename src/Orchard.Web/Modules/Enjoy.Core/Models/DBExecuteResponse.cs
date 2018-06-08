using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Models
{
    public class DbWriteResponse<TModel> : BaseResponse
    {
        public DbWriteResponse(int error, TModel model)
            : base(error)
        {
            this.Model = model;
        }
        public DbWriteResponse(int error) 
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
        public int ErrorCode { get; protected set; }

        public string ErrorMessage { get; protected set; }
        public bool HasError
        {
            get
            {
                return !this.ErrorCode.Equals(EnjoyConstant.Success);
            }
        }
    }

}
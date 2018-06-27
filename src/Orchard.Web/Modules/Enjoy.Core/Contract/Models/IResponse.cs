using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core
{
    public interface IResponse
    {
        [JsonProperty("errcode")]
        int ErrorCode { get; }

        [JsonProperty("errmsg")]
        string ErrorMessage { get; }

        bool HasError { get; }
    }

}
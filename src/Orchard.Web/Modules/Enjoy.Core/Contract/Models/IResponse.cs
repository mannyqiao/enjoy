using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core
{
    public interface IResponse
    {
        [JsonProperty("error_code")]
        int ErrorCode { get; }

        [JsonProperty("error_message")]
        string ErrorMessage { get; }

        bool HasError { get;}
    }
}
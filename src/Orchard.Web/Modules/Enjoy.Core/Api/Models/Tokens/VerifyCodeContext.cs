using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.ApiModels
{
    public class VerifyCodeContext
    {
        [JsonProperty("mobile")]
        public string Mobile { get; set; }
        [JsonProperty("verifyCode")]
        public string VerifyCode { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.ApiModels
{
    using Newtonsoft.Json;
    public class PagingX
    {
        
        [JsonProperty("page")]
        public int Page { get; set; }
        [JsonProperty("size")]
        public int PageSize { get; set; }
    }
}
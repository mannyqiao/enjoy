using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.ApiModels
{
    using Newtonsoft.Json;
    public class ShopNearyby
    {
        [JsonProperty("shopId")]
        public long ShopId { get; set; }

        [JsonProperty("lng")]
        public float Lng { get; set; }

        [JsonProperty("lat")]
        public float Lat { get; set; }


        [JsonProperty("shopAddr")]
        public string ShopAddress { get; set; }

        [JsonProperty("shopName")]
        public string ShopName { get; set; }

        [JsonProperty("shopLogo")]
        public string ShopLogo { get; set; }

        [JsonProperty("shopActList")]
        public ShopAct[] ShopActs
        {
            get;set;
        }
    }
}
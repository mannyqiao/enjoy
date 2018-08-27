

namespace Enjoy.Core.EnjoyModels
{
    using Newtonsoft.Json;
    public class Location
    {
        //Latitude and longitude
        /// <summary>
        /// 经纬
        /// </summary>
        [JsonProperty("lat")]
        public float Latitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        [JsonProperty("lng")]
        public float Longitude { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }
    }
}
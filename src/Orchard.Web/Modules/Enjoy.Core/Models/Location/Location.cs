

namespace Enjoy.Core.EnjoyModels
{
    using Newtonsoft.Json;
    public class Location
    {
        public Location() { }
        public Location(float latitude,float longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }
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
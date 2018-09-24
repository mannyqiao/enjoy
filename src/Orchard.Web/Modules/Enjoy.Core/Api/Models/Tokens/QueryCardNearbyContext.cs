



namespace Enjoy.Core.ApiModels
{
    using Enjoy.Core.EnjoyModels;
    using Newtonsoft.Json;
    public class QueryCardNearbyContext
    {
        [JsonProperty("location")]
        public Location Location { get; set; }
        [JsonProperty("condition")]
        public PagingCondition Condition { get; set; }

        [JsonProperty("distance")]
        public float Distance { get; set; }
        
    }
}
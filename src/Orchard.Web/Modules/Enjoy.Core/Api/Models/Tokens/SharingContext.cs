

namespace Enjoy.Core.ApiModels
{
    using Newtonsoft.Json;
    public class SharingContext
    {
        [JsonProperty("appId")]
        public string AppId { get; set; }

        [JsonProperty("sharedBy")]
        public string SharedBy { get; set; }

        [JsonProperty("cardId")]
        public string CardId { get; set; }

        [JsonProperty("mcode")]
        public string MCode { get; set; }            
    }
}
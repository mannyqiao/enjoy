

namespace Enjoy.Core.ApiModels
{
    using Newtonsoft.Json;
    public class ShopAct
    {
        [JsonProperty("iconName")]
        public string IcoName { get; set; }

        [JsonProperty("iconColor")]
        public string IcoColor { get; set; }

        [JsonProperty("actName")]
        public string ActName { get; set; }
    }
}
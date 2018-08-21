

namespace Enjoy.Core.ApiModels
{
    using Newtonsoft.Json;
    public class Banner
    {
        [JsonProperty("pic")]
        public string LogoUrl { get; set; }

        [JsonProperty("linkType")]
        public int LinkType { get; set; }

        [JsonProperty("linkTo")]
        public string LinkTo { get; set; }

        [JsonProperty("linkName")]
        public string LinkName { get; set; }
    }
}
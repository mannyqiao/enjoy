

namespace Enjoy.Core.Models
{
    using Newtonsoft.Json;
    public class SubMerchant
    {
        [JsonProperty("brand_name")]
        public string BrandName { get; set; }

        [JsonProperty("logo_url")]
        public string LogoUrl { get; set; }

        [JsonProperty("app_id", NullValueHandling = NullValueHandling.Ignore)]
        public string AppId { get; set; }

        [JsonProperty("protocol")]
        public string Protocol { get; set; }

        [JsonProperty("agreement_media_id")]
        public string AgreementMediaId { get; set; }

        [JsonProperty("operator_media_id")]
        public string OperatorMediaId { get; set; }

        [JsonProperty("end_time")]
        public long EndTime { get; set; }

        [JsonProperty("primary_category_id")]
        public int PrimaryCategoryId { get; set; }

        [JsonProperty("secondary_category_id")]
        public int SecondaryCategoryId { get; set; }
    }
}
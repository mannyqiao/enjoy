

namespace Enjoy.Core.WeChatModels
{
    using Newtonsoft.Json;
    public class SubMerchantInfo
    {
        [JsonProperty("merchant_id")]
        public long MerchantId { get; set; }
    }
}
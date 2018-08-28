

namespace Enjoy.Core.WeChatModels
{
    using Newtonsoft.Json;
    using System;
    [Serializable]
    public class SubMerchantInfo
    {
        [JsonProperty("merchant_id")]
        public long MerchantId { get; set; }
    }
}
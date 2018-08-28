
namespace Enjoy.Core.WeChatModels
{
    using Newtonsoft.Json;
    using System;
    [Serializable]
    public class Sku
    {
        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}
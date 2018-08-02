
namespace Enjoy.Core.WeChatModels
{
    using Newtonsoft.Json;
    public class Sku
    {
        [JsonProperty("quantity")]
        public decimal Quantity { get; set; }
    }
}

namespace WeChat.Models
{
    using Newtonsoft.Json;
    public class Sku
    {
        [JsonProperty("quantity")]
        public decimal Quantity { get; set; }
    }
}

namespace WeChat.Models
{
    using Newtonsoft.Json;
    public class Sku
    {
        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}
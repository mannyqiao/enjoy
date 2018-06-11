

namespace Enjoy.Core.Models
{
    using Newtonsoft.Json;
    public class CreateCouponWxResponse : WxResponse
    {
        [JsonProperty("card_id")]
        public string CardId { get; set; }
    }
}
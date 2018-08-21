
namespace Enjoy.Core.WeChatModels
{
    using Newtonsoft.Json;
    public class WxShopPhoto
    {
        [JsonProperty("photo_url")]
        public string photo_url { get; set; }
    }
}
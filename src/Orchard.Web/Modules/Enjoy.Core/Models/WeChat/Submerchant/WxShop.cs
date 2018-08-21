

namespace Enjoy.Core.WeChatModels
{
    using Newtonsoft.Json;
    public class WxShop
    {
        [JsonProperty("base_info")]
        public WxShopBaseInfo BaseInfo { get; set; }
    }
}
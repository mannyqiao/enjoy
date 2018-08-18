

namespace Enjoy.Core.WeChatModels
{
    using Newtonsoft.Json;
    public class WxResponseWapper<T> : WxResponse
    {

        [JsonProperty("info")]
        public T Info { get; set; }
    }
    public class WxRequestWapper<T>
    {
        [JsonProperty("info")]
        public T Info { get; set; }
    }
    public class WxCardCoupon<T>
        where T : ICardCoupon
    {
        [JsonProperty("card")]
        public T Card { get; set; }
    }
    public class WxBusinessWapper<T>
    {
        [JsonProperty("business")]
        public T Business { get; set; }
    }
}
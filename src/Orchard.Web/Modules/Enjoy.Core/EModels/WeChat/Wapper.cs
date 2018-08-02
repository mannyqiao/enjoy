

namespace Enjoy.Core.EModels
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
    public class WxCardCouponWapper<T>
        where T : ICardCoupon
    {
        [JsonProperty("card")]
        public T Card { get; set; }
    }
}
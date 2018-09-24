

namespace Enjoy.Core.ApiModels
{
    using Newtonsoft.Json;
    public class CardNearyBy
    {
        [JsonProperty("id")]
        public long Id { get; set; }        
        /// <summary>
        /// 商户名称
        /// </summary>
        [JsonProperty("merchantName")]
        public string MerchantName { get; set; }
        /// <summary>
        /// 会员卡名称
        /// </summary>
        [JsonProperty("brandName")]
        public string BrandName { get; set; }
        /// <summary>
        /// 会员特权
        /// </summary>
        [JsonProperty("privilege")]
        public string Privilege { get; set; }
        /// <summary>
        /// 会员卡 Logo
        /// </summary>
        [JsonProperty("logoUrl")]
        public string LogoUrl { get; set; }
        /// <summary>
        /// 距离
        /// </summary>
        public float Distance { get; set; }
        
    }
}
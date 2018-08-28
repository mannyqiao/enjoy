

namespace Enjoy.Core
{
    using System;
    using Enjoy.Core.WeChatModels;
    using Newtonsoft.Json;
    public interface ICardCoupon : ICloneable
    {

        [JsonProperty("base_info")]
        BaseInfo BaseInfo { get; set; }

        [JsonProperty("advanced_info")]
        AdvancedInfo AdvancedInfo { get; set; }

        [JsonProperty("card_id", NullValueHandling = NullValueHandling.Ignore)]
        string CardId { get; set; }


        CardTypes CardType { get; set; }

        void Set(Action<ICardCoupon> action);
        /// <summary>
        /// 正对卡券修改做一些必要的转换
        /// </summary>
        /// <returns></returns>
        ICardCoupon TransformForUpgrade();

    }
}
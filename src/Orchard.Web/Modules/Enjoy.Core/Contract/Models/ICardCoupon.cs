

namespace Enjoy.Core
{
    using System;
    using Enjoy.Core.WeChatModels;
    using Newtonsoft.Json;  
    public interface ICardCoupon
    {

        [JsonProperty("base_info")]
        BaseInfo BaseInfo { get; set; }

        [JsonProperty("advanced_info")]
        AdvancedInfo AdvancedInfo { get; set; }

        [JsonProperty("card_id")]
        string CardId { get; set; }

       
        CardTypes CardType { get; set; }

        void Set(Action<ICardCoupon> action);

    }
}
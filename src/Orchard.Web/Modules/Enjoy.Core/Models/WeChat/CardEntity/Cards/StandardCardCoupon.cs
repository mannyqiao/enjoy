namespace Enjoy.Core.WeChatModels
{
    using Newtonsoft.Json;
    using Enjoy.Core;
    using System;
    public abstract class StandardCardCoupon : ICardCoupon
    {

        [JsonProperty("base_info")]
        public BaseInfo BaseInfo { get; set; }

        [JsonProperty("advanced_info")]
        public AdvancedInfo AdvancedInfo { get; set; }

        public string CardId { get; set; }

        private CardTypes type = CardTypes.None;
        [JsonIgnore]
        public CardTypes CardType
        {
            get
            {
                return this.type;
            }
            set
            {
                type = value;
                this.CardTypeName = value.ToString();
            }
        }

        [JsonProperty("card_type")]
        public string CardTypeName
        {
            get; private set;
        }


        public virtual void Set(Action<ICardCoupon> action)
        {
            action(this);
        }

    }
}
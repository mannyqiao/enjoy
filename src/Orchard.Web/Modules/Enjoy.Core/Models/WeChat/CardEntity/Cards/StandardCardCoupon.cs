namespace Enjoy.Core.WeChatModels
{
    using Newtonsoft.Json;
    using Enjoy.Core;
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    [Serializable]
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

        [JsonIgnore]
        public string CardTypeName
        {
            get; private set;
        }


        public virtual void Set(Action<ICardCoupon> action)
        {
            action(this);
        }

        public ICardCoupon TransformForUpgrade()
        {
            var cardcoupon = this.Clone() as ICardCoupon;
            cardcoupon.BaseInfo.Merchant = null;
            cardcoupon.BaseInfo.BrandName = null;
            cardcoupon.BaseInfo.Dateinfo = null;
            cardcoupon.BaseInfo.BindOpenid = null;
            return cardcoupon;
        }

        public object Clone()
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
            stream.Position = 0;
            return formatter.Deserialize(stream) as ICardCoupon;
        }
    }
}


namespace Enjoy.Core.WeChatModels
{
    using System;
    using Newtonsoft.Json;
    public  class CardCoupon<TStandard>
        where TStandard : ICardCoupon

    {
        [JsonProperty("card")]
        public virtual TStandard Card { get; set; }

        [JsonIgnore]
        public CardTypes CardType
        {
            get
            {
                if (Enum.TryParse<CardTypes>(this.CardTypeName, out CardTypes type))
                {
                    return type;
                }
                return CardTypes.None;
            }
            set
            {
                this.CardTypeName = value.ToString();
            }
        }

        [JsonProperty("card_type")]
        public string CardTypeName { get; set; }


        [JsonProperty("card_id")]
        public string CardId { get; set; }

        //public static implicit operator CardCoupon<TStandard>(CashWapper v)
        //{
        //    var wapper = new CashWapper();
        //    wapper.Card = v.Card;
        //    wapper.CardType = v.CardType;
        //    wapper.CardId = v.CardId;
        //    return wapper;
        //}
        //public static implicit operator CardCoupon<TStandard>(DiscountWapper v)
        //{
        //    return new CardCoupon<DiscountCoupon>()
        //    {
        //        Card = v.Card as IStandardCardCoupon,
        //        CardId = v.CardId,
        //        CardType = v.CardType
        //    };

        //}
        //public static implicit operator CardCoupon<TStandard>(GiftWapper v)
        //{

        //    return v;
        //}

        //public static implicit operator CardCoupon<TStandard>(MemberCardWapper v)
        //{
        //    return v;
        //}

        //public static implicit operator CardCoupon<TStandard>(GrouponWapper v)
        //{
        //    return v;
        //}

        //public static implicit operator CardCoupon<TStandard>(GeneralWapper v)
        //{
        //    return v;
        //}
    }


}
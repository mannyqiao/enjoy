

namespace Enjoy.Core
{
    using System;
    using Enjoy.Core.WeChatModels;
    public interface ICardCoupon
    {
        string CardType { get; }

        void Specific(Action<BaseInfo, AdvancedInfo> action);
        void SetCardId(string cardid);
    }
}
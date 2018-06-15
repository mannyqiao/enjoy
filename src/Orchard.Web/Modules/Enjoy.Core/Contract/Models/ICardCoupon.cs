

namespace Enjoy.Core
{
    using System;
    using WeChat.Models;
    public interface ICardCoupon
    {
        string CardType { get; }

        void Specific(Action<BaseInfo, AdvancedInfo> action);
    }
}
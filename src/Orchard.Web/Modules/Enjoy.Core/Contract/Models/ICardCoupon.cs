using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeChat.Models;

namespace Enjoy.Core
{
    public interface ICardCoupon
    {
        string CardType { get; }

        void Specific(Action<BaseInfo, AdvancedInfo> action);
    }
}
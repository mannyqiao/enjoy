

namespace Enjoy.Core
{
    using System;
    using WeChat.Models;
    public interface IWxAccessToken
    {
        string Token { get; }
        int Expiresin { get; }
    }
}

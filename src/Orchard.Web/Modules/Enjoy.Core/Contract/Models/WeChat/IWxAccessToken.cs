

namespace Enjoy.Core
{
    using System;

    public interface IWxAccessToken
    {
        string Token { get; }
        int Expiresin { get; }
        string OpenId { get; }
        string RefreshToken { get; }
        string Scope { get;  }
        IWxLoginUser LoginUser { get; }
        
    }
}

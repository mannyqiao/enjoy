

namespace Enjoy.Core
{
    using System;
  
    public interface IWxAccessToken
    {
        string Token { get; }
        int Expiresin { get; }
    }
}

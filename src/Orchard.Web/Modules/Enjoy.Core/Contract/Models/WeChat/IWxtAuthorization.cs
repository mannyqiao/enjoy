

namespace Enjoy.Core
{
    using System;
   
    public interface IWxAuthorization
    {

        string SessionKey { get; }

        string OpenId { get; }

        int ExpiresIn { get; }
        //{"session_key":"0FVZtH9YGzUAUtUnwkFmVA==","expires_in":7200,"openid":"oCVAR0RTOZ-478wE8-FXfnK-fjFU"}
    }
}

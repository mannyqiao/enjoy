using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Enjoy.Core.Api
{
    [Authorize]
    public class EnjoyController : ApiController
    {
        private readonly IEnjoyAuthService Auth;
        public EnjoyController(IEnjoyAuthService auth)
        {
            this.Auth = auth;
        }
        public EnjoyUserProfile GetEnjoyUser(string mobile)
        {
            return this.Auth.QueryByMobile(mobile);
        }
    }
}

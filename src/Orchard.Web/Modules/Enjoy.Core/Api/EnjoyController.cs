


namespace Enjoy.Core.Api
{
    using System.Web.Http;
    using Orchard;
    [Authorize]
    public class EnjoyController : ApiController
    {
        private readonly IEnjoyAuthService Auth;
        private readonly IMerchantService Merchant;
        private readonly IOrchardServices OS;
        public EnjoyController(
            IOrchardServices os,
            IEnjoyAuthService auth,
            IMerchantService merchant)
        {
            this.Auth = auth;
            this.OS = os;
            this.Merchant = merchant;
        }
        public EnjoyUserProfile GetEnjoyUser(string mobile)
        {
            return this.Auth.QueryByMobile(mobile);
        }
    }
}

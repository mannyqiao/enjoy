

namespace Enjoy.Core
{
    using Orchard;
    using Enjoy.Core.Models.Records;
    using Enjoy.Core.Models;
    using Enjoy.Core.ViewModels;

    public interface IEnjoyAuthService : IDependency
    {

        void SignIn(IEnjoyUser user, bool createPersistentCookie);
        AuthQueryResponse Auth(string mobile, string password);
        void SignOut();
        AuthQueryResponse SignUp(SignUpViewModel model);
        AuthQueryResponse QueryByMobile(string mobile);

        void SetAuthenticatedUserForRequest(IEnjoyUser user);
        IEnjoyUser GetAuthenticatedUser();

        ActionResponse<VerificationCodeViewModel> GetverificationCode(string mobile);
    }
}
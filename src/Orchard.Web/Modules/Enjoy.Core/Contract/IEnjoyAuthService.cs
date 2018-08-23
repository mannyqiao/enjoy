

namespace Enjoy.Core
{
    using Orchard;
    using Enjoy.Core.Records;
    using Enjoy.Core.EnjoyModels;
    using Enjoy.Core.ViewModels;

    public interface IEnjoyAuthService : IDependency
    {

        void SignIn(IEnjoyUser user, bool createPersistentCookie);
        AuthQueryResponse Auth(string mobile, string password);
        void SignOut();
        AuthQueryResponse SignUp(SignUpViewModel model);
        AuthQueryResponse QueryByMobile(string mobile);
        AuthQueryResponse QueryWxUserByMobile(string mobile);
        void SetAuthenticatedUserForRequest(IEnjoyUser user);
        IEnjoyUser GetAuthenticatedUser();

        ActionResponse<VerificationCodeViewModel> GetverificationCode(string mobile, VerifyTypes type);

        bool IsEquals(string mobile, string verifyCode);
    }
}


namespace Enjoy.Core
{
    using Orchard;
    using Enjoy.Core.Models.Records;
    using Enjoy.Core.Models;
    using Enjoy.Core.ViewModels;

    public interface IEnjoyAuthService : IDependency
    {
        EnjoyUserProfile Auth(string mobile, string passowrd);

        EnjoyUserProfile QueryByMobile(string mobile);
        EnjoyUserProfile SignUp(SignViewModel model);
    }
}
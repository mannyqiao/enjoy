

namespace Enjoy.Core
{
    using Orchard;
    public interface IVerifyCodeGenerator : ISingletonDependency
    {
        string GenerateNewVerifyCode();
    }
}

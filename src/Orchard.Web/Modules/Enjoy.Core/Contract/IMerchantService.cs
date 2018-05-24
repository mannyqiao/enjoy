

namespace Enjoy.Core
{
    using Enjoy.Core.ViewModels;
    using Orchard;
    public interface IMerchantService : IDependency
    {
        void CreateSubMerchant(CreatingSubMerchantViewModel model);
    }
}
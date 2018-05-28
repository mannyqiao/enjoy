

namespace Enjoy.Core
{
    using Enjoy.Core.ViewModels;
    using Orchard;
    public interface IMerchantService : IDependency
    {
        void CreateSubMerchant(SubMerchantViewModel model);
        SubMerchantViewModel GetDefaultSubMerchant();
        SubMerchantViewModel GetSubMerchant(int merchantId);
    }
}
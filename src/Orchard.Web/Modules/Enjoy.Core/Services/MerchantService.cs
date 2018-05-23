


namespace Enjoy.Core
{
    using Enjoy.Core.ViewModels;
    using Orchard;
    
    public class MerchantService : IMerchantService
    {
        private readonly IEnjoyAuthService Auth;
        private readonly IOrchardServices OS;
        private ModelClient client = new ModelClient();
        public MerchantService(IOrchardServices os,IEnjoyAuthService auth)
        {
            this.Auth = auth;
            this.OS = os;
        }
        public void CreateSubMerchant(CreatingSubMerchantViewModel model)
        {
            var merchant = client.Convert(model, this.Auth);
            this.OS.TransactionManager.GetSession().SaveOrUpdate(merchant);

            //post sub merchant to WeChat Api

        }
    }
}
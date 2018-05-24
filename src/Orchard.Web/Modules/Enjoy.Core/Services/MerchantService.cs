


namespace Enjoy.Core
{
    using Enjoy.Core.ViewModels;
    using Orchard;

    public class MerchantService : IMerchantService
    {
        private readonly IEnjoyAuthService Auth;
        private readonly IOrchardServices OS;
        public readonly IWeChatApi WeChat;
        private ModelClient client = new ModelClient();
        public MerchantService(
            IOrchardServices os,
            IEnjoyAuthService auth,
            IWeChatApi wechat)
        {
            this.Auth = auth;
            this.OS = os;
            this.WeChat = wechat;
        }
        public void CreateSubMerchant(CreatingSubMerchantViewModel model)
        {
            var merchant = client.Convert(model, this.Auth);
            this.OS.TransactionManager.GetSession().SaveOrUpdate(merchant);
            var submerchant = client.Convert(merchant);
            //post sub merchant to WeChat Api
            var response = this.WeChat.CreateSubmerchant(submerchant);
            if (response.ErrCode == 0)
            {
                merchant.MerchantId = response.Info.MerchantId;
            }
            else
            {
                this.OS.TransactionManager.Cancel();
            }

        }
    }
}
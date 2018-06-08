


namespace Enjoy.Core.Services
{
    using System;
    using Enjoy.Core.ViewModels;
    using NHibernate;
    using Orchard;
    using Enjoy.Core.Models.Records;
    using System.Linq;
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
        public void CreateSubMerchant(SubMerchantViewModel model)
        {
            var session = this.OS.TransactionManager.GetSession();
            var merchant = client.Convert(model, this.Auth);
            session.SaveOrUpdate(merchant);
            session.SaveOrUpdate(client.Convert(merchant, this.Auth.GetAuthenticatedUser()));

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

        public SubMerchantViewModel GetDefaultSubMerchant()
        {
            var user = this.Auth.GetAuthenticatedUser();
            var session = this.OS.TransactionManager.GetSession();
            var merchant = session.QueryOver<Merchant>()
                .Where(o => o.EnjoyUser.Id == user.Id)
                .SingleOrDefault<Merchant>();
            return new SubMerchantViewModel(merchant);
        }

        public SubMerchantViewModel GetSubMerchant(int merchantId)
        {
            throw new NotImplementedException();
        }
    }
}
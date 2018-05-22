
namespace Enjoy.Core.Services
{
    using Orchard;
    using NHibernate;
    using Enjoy.Core.Models.Records;
    using Enjoy.Core.Models;
    using Enjoy.Core.ViewModels;
    using System;
    using Enjoy.Core;
    using System.Collections.Generic;
    using System.Linq;
    using Orchard.Security;
    using System.Text;
    using NHibernate.Linq;
    public class EnjoyAuthService : IEnjoyAuthService
    {
        private readonly IOrchardServices OS;
        private readonly Orchard.Security.IEncryptionService Encryption;
        public EnjoyAuthService(IOrchardServices os, Orchard.Security.IEncryptionService encryption)
        {
            this.OS = os;
            this.Encryption = encryption;
        }


        public EnjoyUserProfile Auth(string mobile, string passowrd)
        {
            var profile = QueryByMobile(mobile);
            if (profile.ErrorCode == EnjoyConstant.Success)
            {
                var pas = Convert.ToBase64String(this.Encryption.Encode(UTF8Encoding.Default.GetBytes(passowrd)));
                if (profile.GetSigleOrDefault().Password.Equals(pas) == false)
                    profile.ErrorCode = EnjoyConstant.IncorrectPasword;
            }
            return profile;
        }

        public EnjoyUserProfile QueryByMobile(string mobile)
        {
            var models = this.OS.TransactionManager.GetSession().QueryOver<EnjoyUser>()
            .Where(o => o.Mobile == mobile).List<EnjoyUser>();
            var result = new EnjoyUserProfile(models);
            return result;
        }

        public EnjoyUserProfile SignUp(SignUpViewModel model)
        {

            if (string.IsNullOrEmpty(model.Password))
            {
                var result = new EnjoyUserProfile();
                result.ErrorCode = EnjoyConstant.PasswordCantBeNullOrEmpty;
                return result;

            }
            if (model.Password.Equals(model.ConfirmPassword) == false)
            {
                var result = new EnjoyUserProfile();
                result.ErrorCode = EnjoyConstant.ConfirPasswordIncorrent;
                return result;
            }


            try
            {
                var profile = QueryByMobile(model.Mobile);
                if (profile.ErrorCode == EnjoyConstant.EmptyOrNullDataSource)
                {
                    var record = new EnjoyUser()
                    {
                        Mobile = model.Mobile,
                        Password = Convert.ToBase64String(this.Encryption.Encode(UTF8Encoding.Default.GetBytes(model.Password))),
                        NickName = string.Format("U{0}{1}", model.Mobile.Substring(0, 3), model.Mobile.Substring(model.Mobile.Length - 4, 4)),
                        CreatedTime = DateTime.UtcNow.ToUnixStampDateTime(),
                        LastSign = DateTime.UtcNow.ToUnixStampDateTime(),
                        LastPassword = string.Empty,
                        LastUpdatedTime = DateTime.UtcNow.ToUnixStampDateTime()
                    };
                    this.OS.TransactionManager.GetSession().SaveOrUpdate(record);
                    return new EnjoyUserProfile(new List<EnjoyUser>() { record }) { ErrorCode = EnjoyConstant.Success };
                }
                return new EnjoyUserProfile(EnjoyConstant.MobileBeUsed, string.Empty);
            }
            catch (NHibernate.Exceptions.GenericADOException ex)
            {
                return new EnjoyUserProfile(EnjoyConstant.ErrorMessageNotDefined, ex.Message);
            }
        }


    }
}
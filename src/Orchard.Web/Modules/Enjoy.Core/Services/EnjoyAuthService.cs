
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
    using Orchard.Caching;
    using Orchard.Services;

    public class EnjoyAuthService : IEnjoyAuthService
    {
        private readonly IOrchardServices OS;
        private readonly IEncryptionService Encryption;
        private readonly ICacheManager Cache;
        private readonly IVerifyCodeGenerator VerifyCodeGenerator;
        private readonly IClock Clock;
        public EnjoyAuthService(
            IOrchardServices os,
            IEncryptionService encryption,
            ICacheManager cache,
            IVerifyCodeGenerator generator,
            IClock clock)
        {
            this.OS = os;
            this.Encryption = encryption;
            this.Cache = cache;
            this.VerifyCodeGenerator = generator;
            this.Clock = clock;
        }


        public EnjoyUserProfile Auth(string mobile, string password)
        {
            var profile = QueryByMobileForSignin(mobile);
            if (profile.ErrorCode == EnjoyConstant.Success)
            {
                if (String.Equals(password, Encoding.UTF8.GetString(this.Encryption.Decode(Convert.FromBase64String(profile.GetSigleOrDefault().Password))), StringComparison.Ordinal))
                {
                    //Set current user
                    this.OS.WorkContext.HttpContext.Session["EnjoyCurrentUser"] = profile.GetSigleOrDefault();
                    return profile;
                }
                else
                {
                    return new EnjoyUserProfile(EnjoyConstant.UPasswordNotMatch);
                }
            }
            return profile;
        }

        public VerificationCodeViewModel GetverificationCode(string mobile)
        {
            var result = this.Cache.Get(mobile, ctx =>
            {
                var code = new VerificationCodeViewModel(mobile, this.VerifyCodeGenerator.GenerateNewVerifyCode());
                ctx.Monitor(this.Clock.When(TimeSpan.FromMinutes(2)));
                return code;
            });
            var span = DateTime.UtcNow.Subtract(result.CreatedAt);
            if (result.Sended == false)
            {
                ////发送手机短信
                result.SetSended();//设置发送状态
            }
            return result;
        }

        public EnjoyUserProfile QueryByMobile(string mobile)
        {
            var models = this.OS.TransactionManager.GetSession().QueryOver<EnjoyUser>()
            .Where(o => o.Mobile == mobile).List<EnjoyUser>();
            if (models == null || models.Count.Equals(0))
            {
                return new EnjoyUserProfile(EnjoyConstant.MobileNotExists);
            }
            else
            {
                return new EnjoyUserProfile(EnjoyConstant.MobileExists);
            }
        }
        private EnjoyUserProfile QueryByMobileForSignin(string mobile)
        {
            var models = this.OS.TransactionManager.GetSession().QueryOver<EnjoyUser>()
                            .Where(o => o.Mobile == mobile).List<EnjoyUser>();
            if (models == null || models.Count.Equals(0))
            {
                return new EnjoyUserProfile(EnjoyConstant.MobileNotExists);
            }
            else
            {
                return new EnjoyUserProfile(EnjoyConstant.Success, models);
            }
        }
        public EnjoyUserProfile SignUp(SignUpViewModel model)
        {

            if (string.IsNullOrEmpty(model.Password))
            {
                return new EnjoyUserProfile(EnjoyConstant.PasswordCantBeNullOrEmpty);
            }
            if (model.Password.Equals(model.ConfirmPassword) == false)
            {
                return new EnjoyUserProfile(EnjoyConstant.ConfirPasswordIncorrent);
            }

            try
            {
                var profile = QueryByMobile(model.Mobile);
                if (profile.ErrorCode == EnjoyConstant.MobileNotExists)
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
                    return new EnjoyUserProfile(EnjoyConstant.Success, record);
                }
                return new EnjoyUserProfile(EnjoyConstant.MobileExists, string.Empty);
            }
            catch (NHibernate.Exceptions.GenericADOException ex)
            {
                return new EnjoyUserProfile(EnjoyConstant.ErrorMessageNotDefined, ex.Message);
            }
        }

        public EnjoyUser GetAuthenticatedUser()
        {
            return this.OS.WorkContext.HttpContext.Session["EnjoyCurrentUser"] as EnjoyUser;
        }
    }
}
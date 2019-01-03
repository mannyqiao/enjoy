
namespace Enjoy.Core.Services
{
    using Orchard;
    using NHibernate;

    using Enjoy.Core.EnjoyModels;
    using Enjoy.Core.WeChatModels;
    using Enjoy.Core.Records;
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
    using Orchard.Environment.Configuration;
    using Orchard.Mvc;
    using Orchard.Logging;
    using System.Web.Security;
    using System.Web;
    using Orchard.Mvc.Extensions;
    using Orchard.UI.Notify;
    using NHibernate.Criterion;

    public class EnjoyAuthService : IEnjoyAuthService
    {
        private const int _cookieVersion = 3;
        private const string _tenantName = "Enjoy.Vip";
        private readonly ShellSettings _settings;
        private readonly IClock _clock;
        private readonly IMembershipService _membershipService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISslSettingsProvider _sslSettingsProvider;
        private readonly ICacheManager _cache;
        private readonly IVerifyCodeGenerator _codeGenerator;
        private readonly IOrchardServices OS;
        private readonly ISMSHelper _sMSHelper;
        private readonly IEncryptionService _encryption;
        private IEnjoyUser _signedInUser;
        private bool _isAuthenticated;
        public ILogger Logger { get; set; }

        public TimeSpan ExpirationTimeSpan { get; set; }

        public EnjoyAuthService(
            ShellSettings settings,
            IClock clock,
            IMembershipService membershipService,
            IHttpContextAccessor httpContextAccessor,
            ISslSettingsProvider sslSettingsProvider,
            ICacheManager cache,
            IVerifyCodeGenerator codeGenerator,
            IOrchardServices os,
            ISMSHelper sMSHelper,
            IEncryptionService encryptionService)
        {
            _settings = settings;
            _clock = clock;
            _membershipService = membershipService;
            _httpContextAccessor = httpContextAccessor;
            _sslSettingsProvider = sslSettingsProvider;
            _cache = cache;
            _codeGenerator = codeGenerator;
            _sMSHelper = sMSHelper;
            Logger = NullLogger.Instance;
            ExpirationTimeSpan = TimeSpan.FromDays(30);
            _encryption = encryptionService;
            OS = os;
        }

        public AuthQueryResponse Auth(string mobile, string password)
        {
            try
            {
                var user = QueryByMobileForSignin(mobile);

                if (user != null)
                {
                    var clearText = this._encryption.Cleartext(user.Password);
                    if (string.Equals(password, clearText, StringComparison.Ordinal))
                    {
                        this.SignIn(new EnjoyUserModel(user), true);
                    }
                    else
                    {
                        return new AuthQueryResponse(Constants.UPasswordNotMatch);
                    }
                }
                return new AuthQueryResponse(Constants.Success, new EnjoyUserModel(user));
            }
            catch (NullReferenceException ex)
            {
                return new AuthQueryResponse(Constants.UserNotExits);
            }



        }



        private EnjoyUser QueryByMobileForSignin(string mobile)
        {
            var models = this.OS.TransactionManager.GetSession().QueryOver<EnjoyUser>()
                            .Where(o => o.Mobile == mobile).List<EnjoyUser>();
            return models.FirstOrDefault();
        }




        #region members's of IEnjoyAuthService
        public void SignIn(IEnjoyUser user, bool createPersistentCookie)
        {
            var now = _clock.UtcNow.ToLocalTime();

            // The cookie user data is "{userName.Base64};{tenant}".
            // The username is encoded to Base64 to prevent collisions with the ';' seprarator.
            var userData = String.Concat(user.Mobile.ToBase64(), ";", _tenantName);

            var ticket = new FormsAuthenticationTicket(
                _cookieVersion,
                user.Mobile,
                now,
                now.Add(ExpirationTimeSpan),
                createPersistentCookie,
                userData,
                FormsAuthentication.FormsCookiePath);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                HttpOnly = true,
                Secure = _sslSettingsProvider.GetRequiresSSL(),
                Path = FormsAuthentication.FormsCookiePath
            };

            var httpContext = _httpContextAccessor.Current();

            if (!String.IsNullOrEmpty(_settings.RequestUrlPrefix))
            {
                cookie.Path = GetCookiePath(httpContext);
            }

            if (FormsAuthentication.CookieDomain != null)
            {
                cookie.Domain = FormsAuthentication.CookieDomain;
            }

            if (createPersistentCookie)
            {
                cookie.Expires = ticket.Expiration;
            }

            httpContext.Response.Cookies.Add(cookie);

            _isAuthenticated = true;
            _signedInUser = user;
        }

        public void SignOut()
        {
            _signedInUser = null;
            _isAuthenticated = false;
            FormsAuthentication.SignOut();

            // overwritting the authentication cookie for the given tenant
            var httpContext = _httpContextAccessor.Current();
            var rFormsCookie = new HttpCookie(FormsAuthentication.FormsCookieName, "")
            {
                Expires = DateTime.UtcNow.AddYears(-1),
            };

            if (!String.IsNullOrEmpty(_settings.RequestUrlPrefix))
            {
                rFormsCookie.Path = GetCookiePath(httpContext);
            }
            httpContext.Response.Cookies.Add(rFormsCookie);
        }
        public AuthQueryResponse SignUp(SignUpViewModel model)
        {

            if (string.IsNullOrEmpty(model.Password))
            {
                return new AuthQueryResponse(Constants.PasswordCantBeNullOrEmpty);
            }
            if (model.Password.Equals(model.ConfirmPassword) == false)
            {
                return new AuthQueryResponse(Constants.ConfirPasswordIncorrent);
            }

            try
            {
                var profile = QueryByMobile(model.Mobile);
                if (profile.ErrorCode == Constants.MobileNotExists)
                {
                    var record = new EnjoyUser()
                    {
                        Mobile = model.Mobile,
                        Password = this._encryption.Ciphertext(model.Password),
                        NickName = string.Format("U{0}{1}", model.Mobile.Substring(0, 3), model.Mobile.Substring(model.Mobile.Length - 4, 4)),
                        CreatedTime = DateTime.UtcNow.ToUnixStampDateTime(),
                        LastActivityTime = DateTime.UtcNow.ToUnixStampDateTime(),
                        LastPassword = string.Empty
                    };
                    this.OS.TransactionManager.GetSession().SaveOrUpdate(record);
#if DEBUG

#else
                   this.SignIn(new EnjoyUserModel(record), true);
#endif
                    return new AuthQueryResponse(Constants.Success, new EnjoyUserModel(record));
                }
                return new AuthQueryResponse(Constants.MobileExists, string.Empty);
            }
            catch (NHibernate.Exceptions.GenericADOException ex)
            {
                return new AuthQueryResponse(Constants.ErrorMessageNotDefined, ex.Message);
            }
        }

        public AuthQueryResponse QueryByMobile(string mobile)
        {
            var models = this.OS.TransactionManager.GetSession().QueryOver<EnjoyUser>()
            .Where(o => o.Mobile == mobile).List<EnjoyUser>();
            if (models == null || models.Count.Equals(0))
            {
                return new AuthQueryResponse(Constants.MobileNotExists);
            }
            else
            {
                return new AuthQueryResponse(Constants.MobileExists);
            }
        }
        public AuthQueryResponse QueryWxUserByMobile(string mobile)
        {
            var models = this.OS.TransactionManager.GetSession().QueryOver<Records.WxUser>()
                .Where(o => o.Mobile == mobile).List();
            if (models == null || models.Count.Equals(0))
            {
                return new AuthQueryResponse(Constants.MobileNotExists);
            }
            else
            {
                return new AuthQueryResponse(Constants.MobileExists);
            }
        }
        public void SetAuthenticatedUserForRequest(IEnjoyUser user)
        {
            _signedInUser = user;
            _isAuthenticated = true;
            //_isNonOrchardUser = false;
        }

        IEnjoyUser IEnjoyAuthService.GetAuthenticatedUser()
        {

            if (_signedInUser != null || _isAuthenticated)
                return _signedInUser;

            var httpContext = _httpContextAccessor.Current();
            if (httpContext.IsBackgroundContext() || !httpContext.Request.IsAuthenticated || !(httpContext.User.Identity is FormsIdentity))
            {
                return null;
            }

            var formsIdentity = (FormsIdentity)httpContext.User.Identity;
            var userData = formsIdentity.Ticket.UserData ?? "";

            // The cookie user data is {userName.Base64};{tenant}.
            var userDataSegments = userData.Split(';');

            if (userDataSegments.Length < 2)
            {
                return null;
            }

            var userDataName = userDataSegments[0];
            var userDataTenant = userDataSegments[1];

            try
            {
                userDataName = userDataName.FromBase64();
            }
            catch
            {
                return null;
            }

            if (!String.Equals(userDataTenant, _tenantName, StringComparison.Ordinal))
            {
                return null;
            }
            var record = QueryByMobileForSignin(userDataName);
            _signedInUser = object.Equals(record, null) ? null : new EnjoyUserModel(record);
            if (_signedInUser == null)////TODO:最好加入登出时间的验证，用于判断是否可以创建cookie.
            {
                //_isNonOrchardUser = true;
                return null;
            }

            _isAuthenticated = true;
            return _signedInUser;
        }

        public ActionResponse<VerificationCodeViewModel> GetverificationCode(string mobile, VerifyTypes type)
        {
            bool firstRequest = false;
            var checkMobile = new AuthQueryResponse(Constants.Success);
            switch (type)
            {
                case VerifyTypes.SignUp:
                    checkMobile = QueryByMobile(mobile);
                    break;
                case VerifyTypes.BindWeChatUser:
                    checkMobile = QueryWxUserByMobile(mobile);
                    break;
                case VerifyTypes.Withdraw:
                case VerifyTypes.FindPassword:
                    throw new NotImplementedException(type.ToString());

            }

            if (checkMobile.ErrorCode == Constants.MobileExists)
            {
                return new ActionResponse<VerificationCodeViewModel>(Constants.MobileExists);
            }
            var result = GetVerificationCode(mobile, true);
            var span = DateTime.UtcNow.Subtract(result.CreatedAt);
            if (span.TotalMinutes <= 2 && result.RequestCount > 1)
            {
                return new ActionResponse<VerificationCodeViewModel>(Constants.FrequencyLimit);
            }
            return new ActionResponse<VerificationCodeViewModel>(Constants.Success, result);
        }
        #endregion

        private string GetCookiePath(HttpContextBase httpContext)
        {
            var cookiePath = httpContext.Request.ApplicationPath;
            if (cookiePath != null && cookiePath.Length > 1)
            {
                cookiePath += '/';
            }

            cookiePath += _settings.RequestUrlPrefix;

            return cookiePath;
        }

        public bool IsEquals(string mobile, string verifyCode)
        {
            return GetVerificationCode(mobile, false).Code.Equals(verifyCode);
        }

        VerificationCodeViewModel GetVerificationCode(string mobile, bool sendViaSMS)
        {
            var model = this._cache.Get<string, VerificationCodeViewModel>(mobile, ctx =>
            {
                var code = new VerificationCodeViewModel(mobile, this._codeGenerator.GenerateNewVerifyCode());
                ctx.Monitor(this._clock.When(TimeSpan.FromMinutes(2)));
                if (sendViaSMS)
                    this._sMSHelper.Send(new QCloudSMS(mobile, SMSNotifyTypes.VerifyCode, code.Code, 2.ToString()));
                return code;
            });
            return model.Request();
        }

        public VirtualAccount CreateVirtualAccountIfNotExists(IWeChatCardKey cardKey)
        {
            var session = this.OS.TransactionManager.GetSession();
            var criteria = session.CreateCriteria<VirtualAccount>();
            criteria.Add(Expression.Eq("AppId", cardKey.AppId));
            criteria.Add(Expression.Eq("OpenId", cardKey.OpenId));
            criteria.Add(Expression.Eq("CardId", cardKey.CardId));
            criteria.Add(Expression.Eq("Code", cardKey.Code));
            var va = criteria.UniqueResult<VirtualAccount>();
            if (va == null)
            {
                va = new VirtualAccount()
                {
                    AppId = cardKey.AppId,
                    OpenId = cardKey.OpenId,
                    CardId = cardKey.CardId,
                    LastUpdatedTime = DateTime.Now.ToUnixStampDateTime(),
                    LastTrading = null,
                    State = VAStates.Normal,
                    Code = cardKey.Code,
                    Money = 0,                     
                };               
            }
            return va;
        }



        //public bool CanAuthenticateWithCookie(IUser user)
        //{
        //    var userPart = user as UserPart;

        //    if (userPart == null)
        //    {
        //        return false;
        //    }

        //    // user has not been approved or is currently disabled
        //    if (userPart.RegistrationStatus != UserStatus.Approved)
        //    {
        //        return false;
        //    }

        //    // if the user has logged out, a cookie should not be accepted
        //    if (userPart.LastLogoutUtc.HasValue)
        //    {

        //        if (!userPart.LastLoginUtc.HasValue)
        //        {
        //            return true;
        //        }

        //        return userPart.LastLogoutUtc < userPart.LastLoginUtc;
        //    }

        //    return true;
        //}
    }
}
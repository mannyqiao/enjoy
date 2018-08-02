

namespace Enjoy.Core.Controllers
{
    using Enjoy.Core.ViewModels;
    using Orchard;
    using Orchard.Mvc.Extensions;
    using Orchard.Themes;
    using System.Web.Mvc;
    using Enjoy.Core.EnjoyModels;
    [Themed]
    public class AccessController : Controller
    {
        private readonly IOrchardServices OS;
        private readonly IEnjoyAuthService Auth;
        public AccessController(IOrchardServices os, IEnjoyAuthService auth)
        {
            this.OS = os;
            this.Auth = auth;
        }
        // GET: Sign
        public ActionResult Sign(bool signin = true)
        {
            if (this.Auth.GetAuthenticatedUser() != null)
                return this.RedirectLocal("/dashboard/summary");

            // var model = new SignViewModel() { Signin = signin };
            if (signin)
                return View(new SignViewModel() { Signin = true, Current = new SignInViewModel() { } });
            else
                return View(new SignViewModel() { Signin = false, Current = new SignUpViewModel() { } });
        }
        [HttpPost]
        public ActionResult Signin(SignInViewModel model, string returnUrl)
        {
            returnUrl = string.IsNullOrEmpty(returnUrl) ? "/dashboard/summary" : returnUrl;
            if (this.Auth.GetAuthenticatedUser() != null)
                return this.RedirectLocal("/dashboard/summary");

            var result = this.Auth.Auth(model.Mobile, model.Password);
            if (result.ErrorCode == EnjoyConstant.Success)
            {
                return this.RedirectLocal(returnUrl);
            }

            return View("Sign", new SignViewModel()
            {
                Signin = true,
                HasError = true,
                ErrorMessage = "登录失败,用户名或密码不正确!",
                Current = model
            });
        }


        [HttpPost]
        public JsonNetResult GetverificationCode(string mobile)
        {
            return new JsonNetResult()
            {
                Data = this.Auth.GetverificationCode(mobile)
            };
        }
        public JsonResult MobileIsAvailable(string mobile)
        {
            ////TODO: need use JsonNetResult as result type;
            return Json(this.Auth.QueryByMobile(mobile).ErrorCode == EnjoyConstant.MobileNotExists,
                JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonNetResult VerifyCode(string mobile, string code)
        {
            return new JsonNetResult()
            {
                Data = this.Auth.IsEquals(mobile, code)
            };
        }


        [HttpPost]
        public ActionResult SignUp(SignUpViewModel model, string returnUrl = null)
        {
            if (this.Auth.GetAuthenticatedUser() != null)
                return this.RedirectLocal("/dashboard/summary");

            var result = this.Auth.SignUp(model);
            return this.RedirectLocal("/dashboard/summary");
        }
        public ActionResult SignOut()
        {
            this.Auth.SignOut();
            return this.RedirectLocal("/");
        }
    }
}
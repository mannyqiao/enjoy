

namespace Enjoy.Core.Controllers
{
    using Enjoy.Core.ViewModels;
    using Orchard;
    using Orchard.Mvc.Extensions;
    using Orchard.Themes;
    using Enjoy.Core.Models.Records;
    using System.Web.Mvc;
    using Orchard.Caching;
    using System;
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
            // var model = new SignViewModel() { Signin = signin };
            if (signin)
                return View(new SignViewModel() { Signin = true, Current = new SignInViewModel() { } });
            else
                return View(new SignViewModel() { Signin = false, Current = new SignUpViewModel() { } });
        }
        [HttpPost]
        public ActionResult Signin(SignInViewModel model, string ReturnUrl)
        {
            if (this.Auth.Auth(model.Mobile, model.Password).ErrorCode == EnjoyConstant.Success)
            {
                this.OS.WorkContext.HttpContext.Session["Mobile"] = model.Mobile;
                return this.RedirectLocal("/dashboard/summary");
            }
            return View("Sign", new SignViewModel()
            {
                Signin = true,
                HasError = true,
                ErrorMessage = "登录失败,用户名或密码不正确!",
                Current = model
            });
        }


        [HttpGet]
        public JsonResult GetverificationCode(string mobile)
        {
            ////TODO Need use Post method
            return Json(this.Auth.GetverificationCode(mobile), JsonRequestBehavior.AllowGet);
        }
        public JsonResult MobileIsAvailable(string mobile)
        {
            return Json(this.Auth.QueryByMobile(mobile).ErrorCode == EnjoyConstant.MobileNotExists,
                JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SignUp(SignUpViewModel model, string ReturnUrl)
        {

            var result = this.Auth.SignUp(model);
            return this.RedirectLocal("/dashboard/summary");
        }
    }
}
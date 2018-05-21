﻿

namespace Enjoy.Core.Controllers
{
    using Enjoy.Core.ViewModels;
    using Orchard;
    using Orchard.Mvc.Extensions;
    using Orchard.Themes;
    using Enjoy.Core.Models.Records;
    using System.Web.Mvc;
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
        [HttpPost]
        public string GetverificationCode(string mobile)
        {
            //// security policy 
            return string.Empty;
        }
        [HttpPost]
        public ActionResult SignUp(SignUpViewModel model, string ReturnUrl)
        {

            var result = this.Auth.SignUp(model);

            return this.RedirectLocal("/dashboard/summary");
        }
    }
}
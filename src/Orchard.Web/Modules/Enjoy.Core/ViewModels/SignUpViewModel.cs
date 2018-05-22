using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Enjoy.Core.ViewModels
{
    public class SignUpViewModel 
    {
        [Required(ErrorMessage = "必填")]
        [Display(Name = "手机号")]
        [RegularExpression(@"^1[3458][0-9]{9}$", ErrorMessage = "手机号格式不正确")]
        [StringLength(11, ErrorMessage = "请输入11位手机号码")]
        public string Mobile { get; set; }

        public string VerificationCode { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public bool Signin { get; set; }
    }
}
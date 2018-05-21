using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.ViewModels
{
    public abstract class BaseViewModel
    {
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }
    }
}
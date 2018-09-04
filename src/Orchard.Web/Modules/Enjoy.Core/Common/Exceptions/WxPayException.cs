using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core
{
    public class WxPayException : Exception
    {
        public WxPayException(string message) 
            : base(message)
        {

        }
    }
}
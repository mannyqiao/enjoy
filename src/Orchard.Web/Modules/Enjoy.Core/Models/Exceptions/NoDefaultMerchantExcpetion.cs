using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core
{
    public class NoDefaultMerchantExcpetion : Exception
    {
        public NoDefaultMerchantExcpetion() : base("No default merchant.")
        {

        }
    }
    public class CheckMerchantException : Exception
    {
        public CheckMerchantException() : base("Merchant check exception")
        {

        }
    }
}
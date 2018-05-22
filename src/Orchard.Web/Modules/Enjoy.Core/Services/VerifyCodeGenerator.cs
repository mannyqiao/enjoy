using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core
{
    public class VerifyCodeGenerator : IVerifyCodeGenerator
    {
        public Random random = new Random();
        public string GenerateNewVerifyCode()
        {
            return random.Next(100000, 999999).ToString();
        }
    }
}
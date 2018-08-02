
namespace Enjoy.Core.WeChatModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System;
    public class WxLoginUser : IWxLoginUser
    {
        public WxLoginUser(string code, string iv, string data, string signature)
        {
            this.Code = code;
            this.IV = iv;
            this.Data = data;
            this.Signature = signature;
        }
        public string Code { get; private set; }

        public string IV { get; private set; }

        public string Data { get; private set; }
        public string Signature { get; private set; }
    }
}

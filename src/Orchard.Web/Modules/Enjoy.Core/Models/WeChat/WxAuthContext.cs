﻿
namespace Enjoy.Core.WeChatModels
{

    using Enjoy.Core;
    public class WxAuthContext : IWxAuthContext
    {
        public WxAuthContext(string appid, string code,
            string state = null,
            string openid = null,
            string iv = null,
            string data = null,
            string signature = null)
        {
            this.AppId = appid;
            this.Code = code;
            this.IV = iv;
            this.Data = data;
            this.Signature = signature;
            this.State = state;
            this.OpenId = openid;

        }
        public WxAuthContext() { }
        public string AppId { get; set; }
        public string Code { get; private set; }
        public string OpenId { get; private set; }
        public string IV { get; private set; }

        public string Data { get; private set; }
        public string Signature { get; private set; }
        public string State { get; private set; }
    }
}

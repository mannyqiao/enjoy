using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.EModels
{
    public class WxMsgToken : IWxMsgToken
    {
        public WxMsgToken(string signature, string timestamp, string nonce, string reqmsg)
        {            
            this.Signature = signature;
            this.TimeStamp = timestamp;
            this.Nonce = nonce;
            this.ReqMsg = reqmsg;
            this.BizMsgToken = EnjoyConstant.WxBizMsgToken;
            this.EncodingAESKey = EnjoyConstant.EncodingAESKey;
            this.AppId = EnjoyConstant.Miniprogram.AppId;
        }

        public string Signature { get; private set; }

        public string TimeStamp { get; private set; }

        public string Nonce { get; private set; }

        public string ReqMsg { get; private set; }

        public string AppId { get; private set; }

        public string BizMsgToken { get; private set; }

        public string EncodingAESKey { get; private set; }
    }
}
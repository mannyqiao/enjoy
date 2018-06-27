using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core
{
    public interface IWxMsgToken
    {
        //    Logger.Information((new { signature = signature, timestamp = timestamp, nonce = nonce
        //}).ToJson());
        string Signature { get; }
        string TimeStamp { get; }
        string BizMsgToken { get; }
        string EncodingAESKey { get; }
        string Nonce { get; }
        string ReqMsg { get; }
        string AppId { get; }
    }
}
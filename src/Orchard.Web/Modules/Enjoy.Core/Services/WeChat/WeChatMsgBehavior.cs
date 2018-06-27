using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Enjoy.Core.Models;

namespace Enjoy.Core.Services
{
    public class WeChatMsgBehavior : IWeChatMsgBehavior
    {

        public ActionResponse<WeChatMsgModel> Execute(IWxMsgToken token, Func<string, WeChatMsgModel> resolve = null)
        {
            var crypt = new WXBizMsgCrypt(token);
            var reqMsg = string.Empty;
            if (crypt.DecryptMsg(token, ref reqMsg) == 0)
            {
                resolve = resolve ?? Resolve;
                return new ActionResponse<WeChatMsgModel>(0, resolve(reqMsg));
            }
            return null;
        }
        private WeChatMsgModel Resolve(string reqMsg)
        {
            return null;
        }
    }
}
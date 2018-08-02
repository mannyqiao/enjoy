
///https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1451025274
namespace Enjoy.Core.EModels
{
    using System.Xml;
    public abstract class WeChatMsgModel
    {
        /// <summary>
        /// 领券方帐号（一个OpenID）
        /// </summary>
        public string FromUserName { get; set; }

        public string ToUserName { get; set; }

        public long CreateTime { get; set; }

        public MsgTypes MsgType { get; set; }
    }
  
}
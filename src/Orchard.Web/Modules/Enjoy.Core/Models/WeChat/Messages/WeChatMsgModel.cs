
///https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1451025274
namespace Enjoy.Core.Models
{
    using System.Xml;
    public abstract class WeChatMsgModel
    {
        public string FromUserName { get; set; }

        public string ToUserName { get; set; }

        public long CreateTime { get; set; }

        public MsgTypes MsgType { get; set; }
    }
  
}

namespace Enjoy.Core.WeChatModels
{
   
    public class WxSession
    {
        public IWeChatConfig Miniprogram { get; set; }
        public IWxAuthorization Authorization { get; set; }
        public IWxAuthContext LoginUser { get; set; }

        public WeChatUserInfo WeCharUser { get; set; }
    }
}

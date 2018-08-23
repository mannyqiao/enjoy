
namespace Enjoy.Core.WeChatModels
{
   
    public class WxSession
    {
        public IMiniprogram Miniprogram { get; set; }
        public IWxAuthorization Authorization { get; set; }
        public IWxLoginUser LoginUser { get; set; }

        public WeChatUserInfo WeCharUser { get; set; }
    }
}

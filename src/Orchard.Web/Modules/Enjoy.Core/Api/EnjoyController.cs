


namespace Enjoy.Core.Api
{
    using System.Web.Http;
    using Orchard;
    using Enjoy.Core.Models;
    //[Authorize]
    public class EnjoyController : ApiController
    {
        private readonly IEnjoyAuthService Auth;
        private readonly IMerchantService Merchant;
        private readonly IOrchardServices OS;
        private readonly IWeChatApi WeChat;
        public EnjoyController(
            IOrchardServices os,
            IEnjoyAuthService auth,
            IWeChatApi wechat,
            IMerchantService merchant)
        {
            this.Auth = auth;
            this.OS = os;
            this.Merchant = merchant;
            this.WeChat = wechat;
        }

        [HttpGet]
        public EnjoyUserProfile GetEnjoyUser(string mobile)
        {
            return this.Auth.QueryByMobile(mobile);
        }
        [Route("api/enjoy/decode")]
        [HttpGet]
        public WxSession DecodeUserinfo(string code, string iv, string encryptedData, string signature)
        {
            return this.WeChat.CreateWxSession(new WxLoginUser(code, iv, encryptedData, signature));
        }
        
        [Route("api/enjoy/test")]
        [HttpGet]
        public IMiniprogram Test()
        {
            return EnjoyConstant.Miniprogram;
            
        }
    }
}

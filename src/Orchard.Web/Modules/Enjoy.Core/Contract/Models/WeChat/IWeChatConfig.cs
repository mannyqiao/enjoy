

namespace Enjoy.Core
{
    using System;

    public interface IWeChatConfig
    {
        /// <summary>
        /// 小程序或公众号的 Appid
        /// </summary>
        string AppId { get; }
        /// <summary>
        /// 小程序或工作号的 app  secret
        /// </summary>
        string AppSecret { get; }
        /// <summary>
        /// 支付商户号
        /// </summary>
        string MchId { get; set; }
        /// <summary>
        /// 支付密钥
        /// </summary>
        string PayKey { get; set; }


    }
}

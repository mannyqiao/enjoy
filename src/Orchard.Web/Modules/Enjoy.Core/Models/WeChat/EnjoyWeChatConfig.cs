using System;
using System.Collections.Generic;
namespace Enjoy.Core.WeChatModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System;
    public class WeChatConfig : IWeChatConfig
    {
        public WeChatConfig(string appid, string appsecrect, string mchid, string key)
        {
            this.AppId = appid;
            this.AppSecret = appsecrect;
            this.MchId = mchid;
            this.PayKey = key;

        }

        public string AppId { get; private set; }

        public string AppSecret { get; private set; }
        /// <summary>
        /// 支付商户号
        /// </summary>
        public string MchId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PayKey { get; set; }
    }
}

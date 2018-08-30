
namespace Enjoy.Core.WeChatModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System;
    public class Miniprogram : IMiniprogram
    {
        public Miniprogram(string appid, string appsecrect,string mchid,string key)
        {
            this.AppId = appid;
            this.AppSecrect = appsecrect;
            this.MchId = mchid;
            this.Key = key;
            
        }

        public string AppId { get; private set; }

        public string AppSecrect { get; private set; }
        /// <summary>
        /// 支付商户号
        /// </summary>
        public string MchId { get; set; }
        public string Key { get; set; }
    }
}


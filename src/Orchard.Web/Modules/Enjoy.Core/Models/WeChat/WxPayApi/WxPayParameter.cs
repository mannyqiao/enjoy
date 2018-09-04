using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.WeChatModels
{
    using Newtonsoft.Json;
    public class WxPayParameter
    {
        public WxPayParameter() { }
        public WxPayParameter(WxUnifiedorderResponse response)
        {
            this.ReturnCode = response.ReturnCode.Value;
            this.ReturnMsg = response.ReturnMsg.Value;
            this.MchId = response.MchId.Value;
            this.NonceStr = response.NonceStr.Value;
            this.PrepayId = response.PrepayId.Value;
            this.AppId = response.AppId.Value;
            this.Sign = response.Sign.Value;
            this.TradeType = response.TradeType.Value;
        }
        private string return_code;

        [JsonProperty("return_code")]
        public string ReturnCode
        {
            get;set;
        }
        private string return_msg;

        [JsonProperty("return_msg")]
        public string ReturnMsg
        {
            get; set;
        }
        private string appid;

        [JsonProperty("appid")]
        public string AppId
        {
            get; set;
        }

        private string mch_id;

        [JsonProperty("mch_id")]
        public string MchId
        {
            get; set;
        }

        private string nonce_str;

        [JsonProperty("nonce_str")]
        public string NonceStr
        {
            get; set;
        }

        private string sign;

        [JsonProperty("sign")]
        public string Sign
        {
            get; set;
        }
        private string prepay_id;

        [JsonProperty("prepay_id")]
        public string PrepayId
        {
            get; set;
        }

        private string trade_type;

        [JsonProperty("trade_type")]
        public string TradeType
        {
            get; set;
        }
        //        <xml>
        //   <return_code><![CDATA[SUCCESS]]></return_code>
        //   <return_msg><![CDATA[OK]]></return_msg>
        //   <appid><![CDATA[wx2421b1c4370ec43b]]></appid>
        //   <mch_id><![CDATA[10000100]]></mch_id>
        //   <nonce_str><![CDATA[IITRi8Iabbblz1Jc]]></nonce_str>
        //   <sign><![CDATA[7921E432F65EB8ED0CE9755F0E86D72F]]></sign>
        //   <result_code><![CDATA[SUCCESS]]></result_code>
        //   <prepay_id><![CDATA[wx201411101639507cbf6ffd8b0779950874]]></prepay_id>
        //   <trade_type><![CDATA[JSAPI]]></trade_type>
        //</xml>
    }
}
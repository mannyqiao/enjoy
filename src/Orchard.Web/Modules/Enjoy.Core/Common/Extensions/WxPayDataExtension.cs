﻿

namespace Enjoy.Core
{
    using Enjoy.Core.WeChatModels;
    using System.IO;
    using System.Xml.Serialization;
    using System.Web;
    using System;
    using Enjoy.Core.Services;
    using System.Reflection;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using System.Text;
    using System.Linq;

    public static class WxPayDataExtension
    {
        public static bool WithRequired(this WxPayData data, out string errMsg)
        {
            errMsg = string.Empty;
            var result = true;
            if (data == null)
            {
                errMsg += "WxPayData 不能为空;";
                return false;
            }
            if (string.IsNullOrEmpty(data.AppId))
            {
                errMsg += "AppId 不能为空;";
                result = false;
            }
            if (string.IsNullOrEmpty(data.MchId))
            {
                errMsg += "MchId 不能为空;";
                result = false;
            }
            if (string.IsNullOrEmpty(data.NonceStr))
            {
                errMsg += "nonce_str 不能为空;";
                result = false;
            }
            if (string.IsNullOrEmpty(data.Sign))
            {
                errMsg += "sign 不能为空;";
                result = false;
            }
            if (string.IsNullOrEmpty(data.Body))
            {
                errMsg += "Body 不能为空;";
                result = false;
            }
            if (string.IsNullOrEmpty(data.OutTradeNo))
            {
                errMsg += "out_trade_no 不能为空;";
                result = false;
            }
            if ((data.Totalfee ?? 0).Equals(0))
            {
                errMsg += "Totalfee 不能为 0;";
                result = false;
            }
            if ((data.Totalfee ?? 0).Equals(0))
            {
                errMsg += "Totalfee 不能为 0;";
                result = false;
            }
            if (string.IsNullOrEmpty(data.SpbillCreateIp))
            {
                errMsg += "spbill_create_ip 不能为 空;";
                result = false;
            }
            if (string.IsNullOrEmpty(data.TradeType))
            {
                errMsg += "trade_type 不能为 空;";
                result = false;
            }
            if (data.TradeType.Equals("JSAPI"))
            {
                if (string.IsNullOrEmpty(data.OpenId))
                {
                    errMsg += "trade_type 不能为 jsapi时 openid不能为空;";
                    result = false;
                }
            }
            if (data.TradeType.Equals("NATIVE"))
            {
                if (string.IsNullOrEmpty(data.ProductId))
                {
                    errMsg += "trade_type 不能为 NATIVE时 product_id 不能为空;";
                    result = false;
                }
            }
            //if (string.IsNullOrEmpty(data.SceneInfo))
            //{
            //    errMsg = "scene_info 不能为空";
            //    result = false;
            //}
            if (string.IsNullOrEmpty(data.NotifyUrl))
            {
                errMsg += "notify_url 不能为空;";
                result = false;
            }
            return result;
        }
        public static string SerializeToXml(this object data)
        {
            var outString = string.Empty;

            using (MemoryStream ms = new MemoryStream())
            {
                var serializer = new XmlSerializer(data.GetType());
                serializer.Serialize(ms, data);
                byte[] arr = ms.ToArray();
                outString = System.Text.Encoding.UTF8.GetString(arr, 0, arr.Length);
                ms.Close();
            }
            return outString;
        }
        //public static string SerializeToXml(this WxPayData data)
        //{
        //    var strBld = new StringBuilder();
        //    strBld.AppendLine("<xml>\r\n");
        //    foreach (var elm in data.GetType().GetProperties().Select((ctx) =>
        //    {
        //        var elm = ctx.GetCustomAttributes<XmlElementAttribute>().FirstOrDefault();
        //        var v = ctx.GetValue(data);
        //        if (v == null) return null;
        //        return new
        //        {
        //            name = elm.ElementName,
        //            value = v
        //        };
        //    })
        //    .Where(o => o != null)
        //    .OrderBy(o => o.name)
        //    .Select(o => o))
        //    {
        //        strBld.AppendLine(string.Format("<{0}>{1}</{0}>", elm.name, elm.value));
        //    }
        //    strBld.AppendLine("</xml>");
        //    return strBld.ToString();
        //}
        /// <summary>
        /// 生成基础授权URL 只能获取 openid
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static string GenerateBasicAuthorizeUrl(this IWeChatConfig config)
        {
            //scope:应用授权作用域，snsapi_base （不弹出授权页面，直接跳转，只能获取用户openid），
            //snsapi_userinfo （弹出授权页面，可通过openid拿到昵称、性别、所在地。并且， 即使在未关注的情况下，只要用户授权，也能获取其信息 ）
            var redirect_uri = HttpUtility.UrlEncode("https://www.yourc.club/wap/pay");
            var @params = new string[] {
                string.Format("appid={0}",config.AppId),
                string.Format("redirect_uri={0}",redirect_uri),
                string.Format("scope={0}","snsapi_base"),
                string.Format("state={0}","Joey#wechat_redirect"),
                string.Format("response_type={0}","code")

            };
            return string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?{0}",
                string.Join("&", @params));
        }
        /// <summary>
        /// 生成支付预处理的 WxPayData
        /// </summary>
        /// <param name="jsApiPay"></param>
        /// <param name="totalfee"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static WxPayData GenerateUnifiedWxPayData(this JsApiPay jsApiPay)
        {
            var data = new WxPayData();
            RandomGenerator randomGenerator = new RandomGenerator();
            var ran = new Random();
            data.Body = "test";
            data.Attach = "test";
            data.OutTradeNo = string.Format("{0}{1}{2}", Constants.WxConfig.MchId, DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999));// "150848433120180905090411634"; //
            data.TimeStart = DateTime.Now.ToString("yyyyMMddHHmmss");// "20180905090412";// "20180905091412"; //

            data.TimeExpire = DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"); //"20180905091412"; //
            data.AppId = Constants.WxConfig.AppId;
            data.OpenId = jsApiPay.OpenId;
            data.TradeType = "JSAPI";
            data.GoodsTag = "test";
            data.MchId = Constants.WxConfig.MchId;
            data.Totalfee = jsApiPay.TotalFee;
            data.NotifyUrl = "https://www.yourc.club/wap/payr";
            data.SpbillCreateIp = "118.24.139.228";
            data.NonceStr = randomGenerator.GetRandomUInt().ToString();// "1489556328";// 
            data.SignType = WxPayData.SIGN_TYPE_HMAC_SHA256;
            data.Sign = data.MakeSign();
            //data.ProductId = "12235413214070356458058";
            //data.SceneInfo = "";
            if (data.WithRequired(out string errMsg) == false)
            {
                throw new WxPayException(errMsg);
            }


            return data;
        }

        public static string PrepareSign(this WxPayData data)
        {
            var @params = data.GetType().GetProperties().Select((ctx) =>
            {
                var elm = ctx.GetCustomAttributes<XmlElementAttribute>().FirstOrDefault();
                if (elm == null || elm.ElementName.Equals("sign")) return null;

                var v = ctx.GetValue(data);
                if (v == null) return null;
                return new
                {
                    name = elm.ElementName,
                    value = v
                };
            })
            .Where(o => o != null)
            .OrderBy(o => o.name);//必须对参数排序否则签名不正确
            return string.Join("&", @params.Select(o => string.Format("{0}={1}", o.name, o.value)));
        }
        public static string PrepareSign(this WxPayParameter data)
        {
          
            var ignoreProperties = new string[] { "paySign", "return_code", "return_msg" };
            var @params = data.GetType().GetProperties().Select((ctx) =>
            {
                var elm = ctx.GetCustomAttributes<Newtonsoft.Json.JsonPropertyAttribute>().FirstOrDefault();
                if (elm == null || ignoreProperties.Any(o => o.Equals(elm.PropertyName, StringComparison.OrdinalIgnoreCase))) return null;

                var v = ctx.GetValue(data);
                if (v == null) return null;
                return new
                {
                    name = elm.PropertyName,
                    value = v
                };
            })
            .Where(o => o != null)
            .OrderBy(o => o.name);//必须对参数排序否则签名不正确
            return string.Join("&", @params.Select(o => string.Format("{0}={1}", o.name, o.value)));
        }
        public static string MakeSign(this WxPayData data)
        {
            //转url格式
            string str = data.PrepareSign();
            //在string后加入API KEY
            str += "&key=" + Constants.WxConfig.Key;
            // return CalcHMACSHA256Hash(str, Constants.WxConfig.Key).MakeMd5();
            return MakeSign(str, data.SignType);
        }
        public static string MakeSign(this WxPayParameter data)
        {
            var str = data.PrepareSign();
            str += "&key=" + Constants.WxConfig.Key;
            return MakeSign(str, WxPayData.SIGN_TYPE_MD5);
        }
        public static string MakeSign(this string plaintext, string signType)
        {
            if (signType == WxPayData.SIGN_TYPE_MD5)
            {
                var md5 = MD5.Create();
                var bs = md5.ComputeHash(Encoding.UTF8.GetBytes(plaintext));
                var sb = new StringBuilder();
                foreach (byte b in bs)
                {
                    sb.Append(b.ToString("x2"));
                }
                //所有字符转为大写
                return sb.ToString().ToUpper();
            }
            else if (signType == WxPayData.SIGN_TYPE_HMAC_SHA256)
            {
                return CalcHMACSHA256Hash(plaintext, Constants.WxConfig.Key);
            }
            else
            {
                throw new ArgumentException("sign_type 不合法");
            }
        }
        public static string MakeMd5(this string text)
        {
            var md5 = MD5.Create();
            var bs = md5.ComputeHash(Encoding.UTF8.GetBytes(text));
            var sb = new StringBuilder();
            foreach (byte b in bs)
            {
                sb.Append(b.ToString("x2"));
            }
            //所有字符转为大写
            return sb.ToString().ToUpper();
        }
        private static string CalcHMACSHA256Hash(string plaintext, string salt)
        {
            string result = "";
            var enc = Encoding.UTF8;
            byte[] baText2BeHashed = enc.GetBytes(plaintext),
            baSalt = enc.GetBytes(salt);
            System.Security.Cryptography.HMACSHA256 hasher = new HMACSHA256(baSalt);
            byte[] baHashedText = hasher.ComputeHash(baText2BeHashed);
            result = string.Join("", baHashedText.ToList().Select(b => b.ToString("x2")).ToArray());
            return result;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Services
{
    public static class WeChatApiRequestBuilder
    {
        /// <summary>
        /// 生成 登陆授权 Url
        /// </summary>
        /// <param name="appid"></param>
        
        /// <param name="secret"></param>
        /// <returns></returns>
        public static string GenerateWxAuthRequestUrl(string appid, string js_code, string secret)
        {
            
            return string.Format("https://api.weixin.qq.com/sns/jscode2session?appid={0}&js_code={1}&secret={2}&grant_type=authorization_code",
                appid, js_code, secret);
        }
        /// <summary>
        /// 生成卡券 创建 Url
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string GenerateWxCreateCardRequestUrl(string token)
        {
            return string.Format("https://api.weixin.qq.com/card/create?access_token={0}",
                token);

        }
        /// <summary>
        /// 生成 Token 获取 Api Url
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        public static string GenerateWxTokenRequestUrl(string appid, string secret)
        {
            return string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}",
                appid, secret);
        }
        /// <summary>
        /// 生成 创建卡券的 Api Url
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string GenerateWxCreateCardUrl(string token)
        {
            return string.Format("https://api.weixin.qq.com/card/create?access_token={0}", token);
        }
        public static string GenerateWxUpdateCardUrl(string token)
        {
            //https://api.weixin.qq.com/card/update?access_token=TOKEN

            return string.Format("https://api.weixin.qq.com/card/update?access_token={0}", token);
        }
        /// <summary>
        /// 生成图片上传Api Url
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string GenerateWxUploaMediaUrl(string token)
        {
            return GenerateWxUploaMediaUrl(token, MediaUploadTypes.Material);
            //return string.Format("https://api.weixin.qq.com/cgi-bin/media/uploadimg?access_token={0}&type=image", token);
            //                    //https://api.weixin.qq.com/cgi-bin/media/upload?access_token=ACCESS_TOKEN&type=TYPE
        }
        public static string GenerateWxUploaMediaUrl(string token, MediaUploadTypes type)
        {

            switch (type)
            {
                case MediaUploadTypes.AuthMaterial:
                    return string.Format("https://api.weixin.qq.com/cgi-bin/media/upload?access_token={0}&type=image", token);
                default: // Image
                    return string.Format("https://api.weixin.qq.com/cgi-bin/media/uploadimg?access_token={0}", token);

            }
        }
        /// <summary>
        /// 生成创建 测试白名单 Api Url
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string GenerateWxtestwhitelist(string token)
        {
            return string.Format("https://api.weixin.qq.com/card/testwhitelist/set?access_token={0}", token);
        }
        /// <summary>
        /// 生成获取 创建 二维码 的 Api Url
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string GenerateWxQRCodeUrl(string token)
        {
            return string.Format("https://api.weixin.qq.com/card/qrcode/create?access_token={0}", token);
        }
        /// <summary>
        /// 生成查询开发类目 Api Url
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string GenerateWxGetApplyProtocolUrl(string token)
        {
            return string.Format("https://api.weixin.qq.com/card/getapplyprotocol?access_token={0}", token);
        }
        /// <summary>
        /// 生成创建子商户的 Api Url
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string GenerateWxSubmerchantUrl(string token, bool bIsNewSubMerchant)
        {
            if (bIsNewSubMerchant)
            {
                return string.Format("https://api.weixin.qq.com/card/submerchant/submit?access_token={0}", token);
            }
            else
            {
                return string.Format("https://api.weixin.qq.com/card/submerchant/update?access_token={0}", token);
            }
        }

        public static string GenerateImageUrlByMediaId(string mediaid)
        {
            ////Enjoy TODO: need return the true url by media id 
            if (string.IsNullOrEmpty(mediaid))
                return "/themes/enjoytheme/content/images/default.jpg";
            else
            {
                return string.Concat(Constants.Directory_Media_Protocol_ROOT.Replace("~/", "/"), "/", string.Format("{0}.jpg", mediaid));
            }
        }
        public static string GenerateWxQueryGetCardList(string token)
        {
            return string.Format("https://api.weixin.qq.com/card/user/getcardlist?access_token={0}", token);
        }
        public static string GenreateQueryWxUserUrl(string openid, string token)
        {
            return string.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN", token, openid);
        }
        public static string GenerateCheckCardAgentRequest(string token)
        {
            return string.Format("http://api.weixin.qq.com/cgi-bin/component/check_card_agent_qualification?access_token={0}", token);
        }
        /// <summary>
        /// 创建门店
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string GenerateAddpoiRequest(string token)
        {
            return string.Format("http://api.weixin.qq.com/cgi-bin/poi/addpoi?access_token={0}", token);
        }
        /// <summary>
        /// 申请母商户第三方强授权能力，上传资质文件
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string UploadAgentQualification(string token)
        {
            return string.Format("http://api.weixin.qq.com/cgi-bin/component/upload_card_agent_qualification?access_token={0}",token);
        }
        public static string GenrateImageUrlByMediaId(string mediaId)
        {
            ////Enjoy TODO: need return the true url by media id 
            if (string.IsNullOrEmpty(mediaId))
                return "/themes/enjoytheme/content/images/default.jpg";
            else
            {
                return string.Concat(Constants.Directory_Media_Protocol_ROOT.Replace("~/", "/"), "/", string.Format("{0}.jpg", mediaId));
            }
        }
    }
}


namespace Enjoy.Core.WeChatModels
{
    using Newtonsoft.Json;
    using System.ComponentModel;
    using Enjoy.Core;
    using System;
    using System.Linq;
    using System;
    [Serializable]
    public class BaseInfo
    {
        public BaseInfo()
        {

        }

        [JsonProperty("sub_merchant_info", NullValueHandling = NullValueHandling.Ignore)]
        public SubMerchantInfo Merchant { get; set; }

        [Newtonsoft.Json.JsonProperty("logo_url")]
        public virtual string LogoUrl { get; set; }

        [Newtonsoft.Json.JsonProperty("brand_name", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string BrandName { get; set; }

        [Newtonsoft.Json.JsonProperty("code_type")]
        public virtual string CodeType { get; set; }

        [Newtonsoft.Json.JsonProperty("title")]
        public virtual string Title { get; set; }



        [DefaultValue("Color010")]
        [Newtonsoft.Json.JsonProperty("color")]
        public virtual string Color { get; set; }


        [Newtonsoft.Json.JsonProperty("notice")]
        [DefaultValue("使用时向服务员出示此券")]
        public virtual string Notice { get; set; }

        [Newtonsoft.Json.JsonProperty("service_phone")]
        public virtual string ServicePhone { get; set; }

        [Newtonsoft.Json.JsonProperty("description")]
        public virtual string Description { get; set; }

        [Newtonsoft.Json.JsonProperty("date_info", NullValueHandling = NullValueHandling.Ignore)]
        public virtual DateInfo Dateinfo { get; set; }


        [Newtonsoft.Json.JsonProperty("sku")]
        public virtual Sku Sku { get; set; }

        [Newtonsoft.Json.JsonProperty("pay_info", NullValueHandling = NullValueHandling.Ignore)]
        public PayInfo PayInfo { get; set; }



        [Newtonsoft.Json.JsonProperty("location_id_list")]
        public virtual long[] LocationIdList { get; set; }

        [Newtonsoft.Json.JsonProperty("use_limit", NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue(1)]
        public virtual int? Uselimit { get; set; }


        [Newtonsoft.Json.JsonProperty("get_limit", NullValueHandling = NullValueHandling.Ignore)]

        public virtual int Getlimit { get; set; }

        [Newtonsoft.Json.JsonProperty("use_custom_code", NullValueHandling = NullValueHandling.Ignore)]
        public virtual bool UseCustomCode { get; set; }

        [Newtonsoft.Json.JsonProperty("bind_openid", NullValueHandling = NullValueHandling.Ignore)]
        public virtual bool? BindOpenid { get; set; }

        [Newtonsoft.Json.JsonProperty("can_share", NullValueHandling = NullValueHandling.Ignore)]
        public virtual bool? CanShare { get; set; }

        [Newtonsoft.Json.JsonProperty("can_give_friend", NullValueHandling = NullValueHandling.Ignore)]
        public virtual bool? CanGivefriend { get; set; }

        [Newtonsoft.Json.JsonProperty("center_title", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string CenterTitle { get; set; }

        [Newtonsoft.Json.JsonProperty("center_sub_title", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string CenterSubTitle { get; set; }


        [JsonProperty("center_app_brand_user_name", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string CenterAppBrandUserName { get; set; }

        [JsonProperty("center_app_brand_pass", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string CenterAppBrandPass { get; set; }


        [JsonProperty("need_push_on_view")]
        public virtual bool NeedPushOnView { get; set; }

        [Newtonsoft.Json.JsonProperty("center_url", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string CenterUrl { get; set; }

        [Newtonsoft.Json.JsonProperty("custom_url_name", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string CustomUrlName { get; set; }

        [Newtonsoft.Json.JsonProperty("custom_url", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string CustomUrl { get; set; }

        [JsonProperty("custom_app_brand_user_name", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string CustomAppBrandUserName { get; set; }

        [JsonProperty("custom_app_brand_pass", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string CustomAppBrandPass { get; set; }

        [Newtonsoft.Json.JsonProperty("custom_url_sub_title", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string CustomUrlSubTitle { get; set; }

        [Newtonsoft.Json.JsonProperty("promotion_url_name", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string PromotionUrlName { get; set; }


        [Newtonsoft.Json.JsonProperty("promotion_app_brand_user_name", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string PromotionAppBrandUserName { get; set; }


        [Newtonsoft.Json.JsonProperty("promotion_app_brand_pass", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string PromotionAppBrandPass { get; set; }

        [Newtonsoft.Json.JsonProperty("promotion_url", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string PromotionUrl { get; set; }

        [Newtonsoft.Json.JsonProperty("source")]
        public virtual string Source { get; set; }

        /// <summary>
        /// 使用动态码，应用于会员卡支付
        /// </summary>
        /// <remarks>
        /// 1.动态码开头四位数字为1636的18位码，开发者使用自定义code时须避免该号段开头的code；
        ///2.每个动态码有效期为5分钟，开发者扫码5分钟内须及时处理余额变动；
        ///3.微信客户端6.3.23之后的客户端版本才可以支持动态码，在6.3.23之前的版本仍旧显示静态码;
        ///4.若用户使用的是低版本微信，微信会员卡的二维码显示的仍旧是会员卡的卡号，开发者须对这部分码进行兼容。
        /// </remarks>
        [Newtonsoft.Json.JsonProperty("use_dynamic_code", NullValueHandling = NullValueHandling.Ignore)]
        public virtual bool? UseDynamicCode { get; set; }
    }
}
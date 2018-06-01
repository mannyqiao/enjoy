

namespace WeChat.Models
{
    using Newtonsoft.Json;
    using System.ComponentModel;

    public class BaseInfo
    {
        [Newtonsoft.Json.JsonProperty("logo_url")]
        public virtual string LogoUrl { get; set; }

        [Newtonsoft.Json.JsonProperty("brand_name")]
        public virtual string BrandName { get; set; }

        [Newtonsoft.Json.JsonProperty("code_type")]
        public virtual string CodeType { get; set; }

        [Newtonsoft.Json.JsonProperty("title")]
        public virtual string Title { get; set; }

        [Newtonsoft.Json.JsonProperty("color")]

        [DefaultValue("Color010")]
        public virtual string Color { get; set; }


        [Newtonsoft.Json.JsonProperty("notice")]
        [DefaultValue("使用时向服务员出示此券")]
        public virtual string Notice { get; set; }

        [Newtonsoft.Json.JsonProperty("service_phone")]
        public virtual string ServicePhone { get; set; }

        [Newtonsoft.Json.JsonProperty("description")]
        public virtual string Description { get; set; }

        [Newtonsoft.Json.JsonProperty("date_info")]
        public virtual DateInfo Dateinfo { get; set; }


        [Newtonsoft.Json.JsonProperty("sku")]
        public virtual Sku Sku { get; set; }


        [Newtonsoft.Json.JsonProperty("location_id_list")]
        public virtual long[] LocationIdList { get; set; }

        [Newtonsoft.Json.JsonProperty("use_limit")]
        [DefaultValue(1)]
        public virtual int Uselimit { get; set; }


        [Newtonsoft.Json.JsonProperty("get_limit")]
        
        public virtual int Getlimit { get; set; }

        [Newtonsoft.Json.JsonProperty("use_custom_code")]
        public virtual bool UseCustomCode { get; set; }

        [Newtonsoft.Json.JsonProperty("bind_openid")]
        public virtual bool BindOpenid { get; set; }

        [Newtonsoft.Json.JsonProperty("can_share")]
        public virtual bool CanShare { get; set; }

        [Newtonsoft.Json.JsonProperty("can_give_friend")]
        public virtual bool CanGivefriend { get; set; }

        [Newtonsoft.Json.JsonProperty("center_title")]
        public virtual string CenterTitle { get; set; }

        [Newtonsoft.Json.JsonProperty("center_sub_title")]
        public virtual string CenterSubTitle { get; set; }

        [Newtonsoft.Json.JsonProperty("center_url")]
        public virtual string CenterUrl { get; set; }

        [Newtonsoft.Json.JsonProperty("custom_url_name")]
        public virtual string CustomUrlName { get; set; }

        [Newtonsoft.Json.JsonProperty("custom_url")]
        public virtual string CustomUrl { get; set; }

        [Newtonsoft.Json.JsonProperty("custom_url_sub_title")]
        public virtual string CustomUrlSubTitle { get; set; }

        [Newtonsoft.Json.JsonProperty("promotion_url_name")]
        public virtual string PromotionUrlName { get; set; }

        [Newtonsoft.Json.JsonProperty("promotion_url")]
        public virtual string PromotionUrl { get; set; }

        [Newtonsoft.Json.JsonProperty("source")]
        public virtual string Source { get; set; }
    }
}
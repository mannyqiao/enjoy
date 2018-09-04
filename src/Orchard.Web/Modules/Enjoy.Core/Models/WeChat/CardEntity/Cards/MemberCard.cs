namespace Enjoy.Core.WeChatModels
{
    using Newtonsoft.Json;
    using Enjoy.Core;
    using System;
    [Serializable]
    public class MemberCard : StandardCardCoupon
    {
        [JsonProperty("background_pic_url")]
        public string BackgroundPicUrl { get; set; }


        [JsonProperty("bonus_rule", NullValueHandling = NullValueHandling.Ignore)]
        public BonusRule BonusRule { get; set; }

        [JsonProperty("supply_bonus")]
        public bool SupplyBonus { get; set; }


        [JsonProperty("supply_balance")]
        public bool SupplyBanlance { get; set; }

     

        [JsonProperty("prerogative")]
        public string Prerogative { get; set; }

        [JsonProperty("auto_activate")]
        public bool AutoActivate { get; set; }

   
  

        [JsonProperty("custom_field1",NullValueHandling = NullValueHandling.Ignore)]
        public CustomField CustomField1 { get; set; }

        [JsonProperty("activate_url",NullValueHandling = NullValueHandling.Ignore)]
        public string ActivateUrl { get; set; }

        [JsonProperty("custom_cell1",NullValueHandling = NullValueHandling.Ignore)]
        public CustomCell CustomCell { get; set; }
        private int discount;

        [JsonProperty("wx_activate",NullValueHandling = NullValueHandling.Ignore)]
        public bool? WxActivate { get; set; }
        /// <summary>
        /// 	折扣，该会员卡享受的折扣优惠,填10就是九折。
        /// </summary>
        [JsonProperty("discount")]
        public int Discount
        {
            get
            {
                return this.discount / 10;

            }
            set
            {
                this.discount = value * 10;

            }
        }
        [Newtonsoft.Json.JsonProperty("activate_app_brand_user_name")]
        public string ActivateAppBrandUserName { get; set; }

        [Newtonsoft.Json.JsonProperty("activate_app_brand_pass")]
        public string ActivateAppBrandPass { get; set; }
        //"supply_bonus": true,
        //  "supply_balance": false,
        //  "prerogative": "test_prerogative",
        //  "auto_activate": true,
        //  "custom_field1": {
        //      "name_type": "FIELD_NAME_TYPE_LEVEL",
        //      "url": "http://www.qq.com"
        //  },
        //  "activate_url": "http://www.qq.com",
        //  "custom_cell1": {
        //      "name": "使用入口2",
        //      "tips": "激活后显示",
        //      "url": "http://www.xxx.com"
        //  },
        //  "bonus_rule": {
        //      "cost_money_unit": 100,
        //      "increase_bonus": 1,
        //      "max_increase_bonus": 200,
        //      "init_increase_bonus": 10,
        //      "cost_bonus_unit": 5,
        //      "reduce_money": 100,
        //      "least_money_to_use_bonus": 1000,
        //      "max_reduce_bonus": 50
        //  },
        //  "discount": 10


    }
}
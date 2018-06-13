﻿

namespace WeChat.Models
{
    using Enjoy.Core;
    using Newtonsoft.Json;
    public class BonusRule
    {
        /// <summary>
        /// 消费金额。以分为单位。
        /// </summary>
        [JsonProperty("cost_money_unit")]
        public decimal cost_money_unit { get; set; }

        /// <summary>
        /// 对应增加的积分。
        /// </summary>
        [JsonProperty("increase_bonus")]
        public decimal increase_bonus { get; set; }

        /// <summary>
        /// 用户单次可获取的积分上限。
        /// </summary>
        [JsonProperty("max_increase_bonus")]
        public decimal max_increase_bonus { get; set; }

        /// <summary>
        /// 初始设置积分。
        /// </summary>
        [JsonProperty("init_increase_bonus")]
        public decimal init_increase_bonus { get; set; }

        /// <summary>
        /// 每使用5积分。
        /// </summary>
        [JsonProperty("cost_bonus_unit")]
        public decimal cost_bonus_unit { get; set; }

        /// <summary>
        /// 	抵扣xx元，（这里以分为单位）
        /// </summary>
        [JsonProperty("reduce_money")]
        public decimal reduce_money { get; set; }

        /// <summary>
        /// 	抵扣条件，满xx元（这里以分为单位）可用。
        /// </summary>
        [JsonProperty("least_money_to_use_bonus")]
        public decimal least_money_to_use_bonus { get; set; }

        /// <summary>
        /// 	抵扣条件，单笔最多使用xx积分。
        /// </summary>
        [JsonProperty("max_reduce_bonus")]
        public decimal max_reduce_bonus { get; set; }
    
    }
}
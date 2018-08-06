

namespace Enjoy.Core.WeChatModels
{
    using Enjoy.Core;
    using Newtonsoft.Json;
    public class BonusRule
    {
        private int cost_money_unit = 0;
        /// <summary>
        /// 消费金额。以分为单位。
        /// </summary>
        [JsonProperty("cost_money_unit")]
        public int CostMoneyUnit
        {
            get
            {
                return this.cost_money_unit / 100;
            }
            set
            {
                this.cost_money_unit = value * 100;
            }
        }

        /// <summary>
        /// 对应增加的积分。
        /// </summary>
        [JsonProperty("increase_bonus")]
        public int IncreaseBonus
        {
            get; set;
        }

        /// <summary>
        /// 用户单次可获取的积分上限。
        /// </summary>
        [JsonProperty("max_increase_bonus")]
        public int MaxIncreaseBonus { get; set; }

        /// <summary>
        /// 初始设置积分。
        /// </summary>
        [JsonProperty("init_increase_bonus")]
        public int InitIncreaseBonus { get; set; }

        /// <summary>
        /// 每使用5积分。
        /// </summary>
        [JsonProperty("cost_bonus_unit")]
        public int CostBonusUnit { get; set; }

        private int reduce_money;
        /// <summary>
        /// 	抵扣xx元，（这里以分为单位）
        /// </summary>
        [JsonProperty("reduce_money")]
        public int ReduceMoney
        {
            get
            {
                return this.reduce_money / 100;
            }
            set
            {
                this.reduce_money = value * 100;
            }
        }

        private int least_money_to_use_bonus;
        /// <summary>
        /// 	抵扣条件，满xx元（这里以分为单位）可用。
        /// </summary>
        [JsonProperty("least_money_to_use_bonus")]
        public int LeastMoneyToUseBonus
        {
            get
            {
                return this.least_money_to_use_bonus / 100;
            }
            set
            {
                this.least_money_to_use_bonus = value * 100;
            }
        }
        /// <summary>
        /// 	抵扣条件，单笔最多使用xx积分。
        /// </summary>
        [JsonProperty("max_reduce_bonus")]
        public int max_reduce_bonus { get; set; }

    }
}
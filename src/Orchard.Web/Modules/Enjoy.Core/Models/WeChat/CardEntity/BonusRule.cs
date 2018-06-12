

namespace WeChat.Models
{
    using Enjoy.Core;
    using Newtonsoft.Json;
    public class BonusRule
    {
        public decimal cost_money_unit { get; set; }
        public decimal increase_bonus { get; set; }
        public decimal max_increase_bonus { get; set; }
        public decimal init_increase_bonus { get; set; }
        public decimal cost_bonus_unit { get; set; }
        public decimal reduce_money { get; set; }
        public decimal least_money_to_use_bonus { get; set; }

        public decimal max_reduce_bonus { get; set; }
        //"cost_money_unit": 100,
        //      "increase_bonus": 1,
        //      "max_increase_bonus": 200,
        //      "init_increase_bonus": 10,
        //      "cost_bonus_unit": 5,
        //      "reduce_money": 100,
        //      "least_money_to_use_bonus": 1000,
        //      "max_reduce_bonus": 50
    }
}
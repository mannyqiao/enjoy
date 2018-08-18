namespace Enjoy.Core.WeChatModels
{
    using Newtonsoft.Json;
    using Enjoy.Core;
    using System;

    /// <summary>
    /// 现金券
    /// </summary>
    public class CashCoupon : StandardCardCoupon
    {

        #region cash 专用
        private int? last_cost;

        [JsonProperty("least_cost", NullValueHandling = NullValueHandling.Ignore)]
        public int? LeastCost
        {
            get
            {
                return (this.last_cost ?? 0) / 100;
            }
            set
            {
                //this.last_cost = (value ?? 0) * 100;
                this.last_cost = value == null ? null : value * 100;
            }
        }
        private int? reduce_cost;
        [JsonProperty("reduce_cost", NullValueHandling = NullValueHandling.Ignore)]
        public int? ReduceCost
        {
            get
            {
                return (this.reduce_cost ?? 0) / 100;
            }
            set
            {
                //this.reduce_cost = (value ?? 0) * 100;
                this.reduce_cost = value == null ? null : value * 100;
            }
        }
       
        #endregion
    }
}
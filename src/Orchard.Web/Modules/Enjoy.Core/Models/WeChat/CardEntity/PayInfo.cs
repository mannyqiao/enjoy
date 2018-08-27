using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.WeChatModels
{
    using Newtonsoft.Json;
    [Serializable]
    public class PayInfo
    {
        [JsonProperty("swipe_card")]
        public SwipeCard SwipeCard { get; set; }
    }
    [Serializable]
    public class SwipeCard
    {
        [JsonProperty("is_swipe_card")]
        public bool IsSwipeCard
        {
            get; set;
        }
    }
}
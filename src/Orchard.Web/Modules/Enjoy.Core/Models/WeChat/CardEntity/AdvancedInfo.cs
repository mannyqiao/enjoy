﻿

namespace WeChat.Models
{
    using Newtonsoft.Json;
    public class AdvancedInfo
    {
        [Newtonsoft.Json.JsonProperty("use_condition")]
        public UseCondition UseCondition { get; set; }

        [Newtonsoft.Json.JsonProperty("abstract")]
        public Abstract Abstract { get; set; }

        [Newtonsoft.Json.JsonProperty("text_image_list")]
        public TextImage[] TextImageList { get; set; }

        [Newtonsoft.Json.JsonProperty("text_image_list")]
        public string[] BusinessService { get; set; }
    }
}


namespace Enjoy.Core.WeChatModels
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;
    public class AdvancedInfo
    {
        public AdvancedInfo()
        {
           


        }
        [Newtonsoft.Json.JsonProperty("use_condition")]
        public UseCondition UseCondition { get; set; }

        [Newtonsoft.Json.JsonProperty("abstract")]
        public Abstract Abstract { get; set; }

        [Newtonsoft.Json.JsonProperty("text_image_list")]
        public List<TextImage> TextImageList
        {
            get; set;
        }
        [JsonIgnore]
        public List<TextImage> ExTextImageList
        {
            get
            {
                var cnt = this.TextImageList == null
                    ? 0
                    : this.TextImageList.Count;
                var list = this.TextImageList ?? new List<TextImage>();
                for (var j = cnt; j < 3; j++)
                {
                    list.Add(new TextImage() { Text = string.Empty, ImageUrl = string.Empty });
                }
                return list;
            }
            set
            {
                this.TextImageList = value.Where(o => !string.IsNullOrEmpty(o.Text) && !string.IsNullOrEmpty(o.ImageUrl)).ToList();
            }
        }
        [Newtonsoft.Json.JsonProperty("business_service")]
        public string[] BusinessService { get; set; }

        [Newtonsoft.Json.JsonProperty("time_limit", NullValueHandling = NullValueHandling.Ignore)]
        public TimeLimit[] TimeLimits { get; set; }
    }
}
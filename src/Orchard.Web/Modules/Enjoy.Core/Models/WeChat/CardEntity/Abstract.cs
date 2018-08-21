

namespace Enjoy.Core.WeChatModels
{

    using System.Linq;
    using Newtonsoft.Json;
    public class Abstract
    {
        [Newtonsoft.Json.JsonProperty("abstract")]
        public virtual string AbstractX { get; set; }

        
        [Newtonsoft.Json.JsonProperty("icon_url_list")]
        public virtual string[] IconUrlList
        {
            get;
            set;
        }

        [JsonIgnore]
        public string DefaultIcoUrl
        {
            get
            {
                if (this.IconUrlList == null) return string.Empty;
                else return this.IconUrlList.FirstOrDefault();
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    this.IconUrlList = new string[] { };
                else
                    this.IconUrlList = new string[] { value };
            }
        }
    }
}
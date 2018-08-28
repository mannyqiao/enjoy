namespace Enjoy.Core.WeChatModels
{
    using Newtonsoft.Json;
    using System;
    [Serializable]
    public class TextImage
    {
        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }
        
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
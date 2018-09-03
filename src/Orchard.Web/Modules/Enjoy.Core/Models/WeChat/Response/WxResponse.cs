

namespace Enjoy.Core.WeChatModels
{
    using Newtonsoft.Json;
    using System;
    using System.Xml.Serialization;

    
    
    public abstract class WxResponse
    {
    
        [JsonProperty("errcode")]
        public virtual int ErrCode { get; set; }


        
        [JsonProperty("errmsg")]
        public virtual string ErrMsg { get; set; }

        
        [JsonIgnore]
        public virtual bool HasError
        {
            get
            {
                return ErrCode != 0;
            }
        }

    }
    public class NormalWxResponse : WxResponse
    {

    }
}
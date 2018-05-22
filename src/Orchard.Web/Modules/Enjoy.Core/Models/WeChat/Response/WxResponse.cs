using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Models
{
    public abstract class WxResponse
    {

        [Newtonsoft.Json.JsonProperty("errcode")]
        public virtual int ErrCode { get; set; }


        [Newtonsoft.Json.JsonProperty("errmsg")]
        public virtual string ErrMsg { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public virtual bool HasError
        {
            get
            {
                return ErrCode != 0;
            }
        }

    }
}
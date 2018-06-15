
namespace Enjoy.Core.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System;
    public class WxSession
    {
        public IMiniprogram Miniprogram { get; set; }
        public IWxAuthorization Authorization { get; set; }
        public IWxLoginUser LoginUser { get; set; }

        public WxUser WeCharUser { get; set; }
    }
}

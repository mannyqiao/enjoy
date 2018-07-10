using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Models.Records
{
    public class WxMsg : IEntityKey<int>
    {
        public virtual int Id { get; set; }
        public virtual MsgTypes MsgType { get; set; }
        public virtual long CreatedTime { get; set; }
        public virtual string FromUser { get; set; }
        public virtual string ToUser { get; set; }
        
        public virtual string Metadata { get; set; }
    }
}
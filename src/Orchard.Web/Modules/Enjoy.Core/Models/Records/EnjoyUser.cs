using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Records
{
    public class EnjoyUser : IEntityKey<long>
    {
        public virtual long Id { get; set; }
        public virtual string Mobile { get; set; }
        public virtual string NickName { get; set; }
        public virtual WxUser WxUser { get; set; }
        public virtual string Password { get; set; }        
        public virtual string LastPassword { get; set; }        
        public virtual string Profile { get; set; }
        public virtual long CreatedTime { get; set; }
        public virtual long LastActivityTime { get; set; }
    }
}
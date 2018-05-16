using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Models.Records
{
    public class EnjoyUser
    {
        public virtual int Id { get; set; }
        public virtual string Mobile { get; set; }
        public virtual string NickName { get; set; }
        public virtual string Password { get; set; }
        public virtual string WeChatId { get; set; }
        public virtual Gender? Gender { get; set; }
        public virtual string Country { get; set; }
        public virtual string Province { get; set; }
        public virtual string City { get; set; }
        public virtual string LastPassword { get; set; }
        public virtual DateTime LastSign { get; set; }
        public virtual DateTime LastUpdatedTime { get; set; }
        public virtual DateTime CreatedTime { get; set; }
    }
}
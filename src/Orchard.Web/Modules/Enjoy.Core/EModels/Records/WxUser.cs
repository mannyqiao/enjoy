using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Models.Records
{
    public class WxUser : IEntityKey<long>
    {
        public virtual long Id { get; set; }
        public virtual string UnionId { get; set; }
        public virtual string Mobile { get; set; }
        public virtual RegistryTypes RegistryType { get; set; }
        public virtual string NickName { get; set; }
        public virtual string Country { get; set; }
        public virtual string Province { get; set; }
        public virtual string City { get; set; }
        public virtual long CreatedTime { get; set; }
        public virtual long LastActivityTime { get; set; }        
    }
}
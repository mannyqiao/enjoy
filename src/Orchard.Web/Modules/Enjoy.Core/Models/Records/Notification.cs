using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Models.Records
{
    public class Notification : IEntityKey<long>
    {
        public virtual long Id { get; set; }
        public virtual EnjoyUser EnjoyUser { get; set; }
        public virtual string Title { get; set; }
        public virtual long CreatedTime { get; set; }
        public virtual bool SendBySMS { get; set; }
        public virtual bool Read { get; set; }
        public virtual string Body { get; set; }

        public virtual long LastActivityTime { get; set; }
    }
}
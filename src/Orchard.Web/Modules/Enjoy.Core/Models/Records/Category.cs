using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Records
{
    public class Category : IEntityKey<long>
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual Merchant Merchant { get; set; }
        public virtual string Settings { get; set; }

        public long LastActivityTime { get; set; }
    }
}
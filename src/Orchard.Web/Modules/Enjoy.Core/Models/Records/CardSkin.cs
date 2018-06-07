using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Models.Records
{
    public class CardJson 
    {
        public virtual long Id { get; set; }
        public virtual CardTypes CardType { get; set; }
    }
}
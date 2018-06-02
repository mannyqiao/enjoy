using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core
{
    public interface ISegmentOption
    {
        string IdOf();
        string NameOf();
        string Text { get;  }
    }
}
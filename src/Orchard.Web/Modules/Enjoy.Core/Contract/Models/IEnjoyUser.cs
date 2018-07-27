using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core
{
    public interface IEnjoyUser
    {
        long Id { get; }
        string Mobile { get; }
        string NickName { get; }
    }
}
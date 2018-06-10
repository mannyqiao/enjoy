using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core
{
    public interface IEntityKey<T>
    {
        T Id { get; }
    }
}
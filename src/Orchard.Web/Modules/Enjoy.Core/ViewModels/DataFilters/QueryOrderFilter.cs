﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.ViewModels
{
    public class QueryOrderFilter
    {
        public int Column { get; set; }
        public Direction Dir { get; set; }
    }
}
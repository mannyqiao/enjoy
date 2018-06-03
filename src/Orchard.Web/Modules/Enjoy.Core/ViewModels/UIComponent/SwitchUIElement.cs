﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Enjoy.Core.UIElements
{
    public class SwitchUIElement : UIElement
    {
        public SwitchUIElement(string name, string text, bool value = false)
            : base(name, text, false, value.ToString().ToLower(), string.Empty)
        {

        }

    }
}
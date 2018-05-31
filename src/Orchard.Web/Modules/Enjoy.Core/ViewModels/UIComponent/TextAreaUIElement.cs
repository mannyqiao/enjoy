using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.UIElements
{
    public class TextAreaUIElement : UIElement
    {
        public TextAreaUIElement(string name,
         string label,
         string value = null,
         string placeholder = null,
         bool required = false)
         : base(name, label, value, required)
        {
            this.Placeholder = placeholder;
        }
        public string Placeholder { get; set; }
        public override UIType Type { get { return UIType.TextArea; } }
    }
}
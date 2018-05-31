using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.UIElements
{
    public class TextUIElement : UIElement
    {
        public TextUIElement(string id,
            string text,
            string value = null,
            string placeholder = null,
            bool required = false)
            : base(id, text, value, required)
        {
            this.Placeholder = placeholder;
        }
        public override UIType Type
        {
            get
            {
                return UIType.Text;
            }
        }
        public string Placeholder { get; set; }
    }
}
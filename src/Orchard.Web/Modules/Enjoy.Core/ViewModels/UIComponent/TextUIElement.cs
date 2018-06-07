

using System;
using System.Linq.Expressions;

namespace Enjoy.Core.UIElements
{
    public class TextUIElement : UIElement<string>
    {

        public TextUIElement(
            string name,
            string text,
            string value = null,
            int length = 20,
            string placeholder = null,
            string message = null,
            bool required = false)
            : base(name, text, value,required, message)
        {
            this.Placeholder = placeholder;
            this.Length = length;
        }
        public string Placeholder { get; set; }
        public int Length { get; set; }

    }
}
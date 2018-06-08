

using System;
using System.Linq.Expressions;

namespace Enjoy.Core.UIElements
{
    public class TextAreaUIElement : UIElement<string>
    {

        public TextAreaUIElement(
            string name,
            string text,
            string value = null,
            int length = 100,
            bool required = false,
            string placeholder = null,
            string message = null)
            : base(name, text, value, required, message)
        {
            this.Length = length;
            this.Placeholder = placeholder;            
        }
        public string Placeholder { get;protected set; }
        public int Length { get; protected set; }
    }
}
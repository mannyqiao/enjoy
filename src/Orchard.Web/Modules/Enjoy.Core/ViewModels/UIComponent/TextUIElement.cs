

using System;
using System.Linq.Expressions;

namespace Enjoy.Core.UIElements
{
    public class TextUIElement : UIElement
    {

        public TextUIElement(            
            string name,
            string text,
            string value = null,
            string placeholder = null,
            string message = null,
            bool required = false)
            : base(name, text, required, value,message)
        {
            this.Placeholder = placeholder;
           
        }        
        public string Placeholder { get; set; }

    }
}
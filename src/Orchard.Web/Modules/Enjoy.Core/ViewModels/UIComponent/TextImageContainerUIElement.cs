

using System;
using System.Linq.Expressions;
using WeChat.Models;

namespace Enjoy.Core.UIElements
{
    public class TextImageContainerUIElement : UIElement
    {
        public TextImageContainerUIElement(
            string name,
            TextImage[] items,
            string placeholder = null,
            string message = null,
            bool required = false)
            : base(name, string.Empty, required, string.Empty, message)
        {
            this.Placeholder = placeholder;
            this.Items = items;
        }
        public string Placeholder { get; set; }
        public TextImage[] Items { get; set; }
    }
}
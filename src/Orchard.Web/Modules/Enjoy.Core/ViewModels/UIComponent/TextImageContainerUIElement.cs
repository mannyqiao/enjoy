

using System;
using System.Linq.Expressions;
using Enjoy.Core.WeChatModels;

namespace Enjoy.Core.UIElements
{
    public class TextImageContainerUIElement : UIElement<string>
    {
        public TextImageContainerUIElement(
            string name,
            TextImage[] items,
            string placeholder = null,
            string message = null,
            bool required = false)
            : base(name, string.Empty, null)
        {
            this.Placeholder = placeholder;
            this.Items = items;
        }
        public string Placeholder { get; set; }
        public TextImage[] Items { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.UIElements
{
    public class GroupUIElement : ISegmentOption
    {
        public GroupUIElement(string id, string text, UIElement[] elements)
        {
            this.Id = id;
            this.Text = text;
            this.Elements = elements;
        }
        public string Id { get; set; }
        public string Text { get; set; }
        public UIElement[] Elements { get; set; }
    }
}
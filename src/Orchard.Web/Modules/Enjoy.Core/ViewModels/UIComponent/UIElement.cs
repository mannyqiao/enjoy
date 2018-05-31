using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.UIElements
{
    public abstract class UIElement : ISegmentOption
    {
        public UIElement(
            string id,
            string text,
            string value = null,
            bool required = false)
        {
            this.Text =text;
            this.Id = id;
            this.Value = value;
            this.Required = required;
        }
        public virtual UIType Type { get { throw new NotImplementedException("Must override by sub class."); } }
        public string Text { get; set; }
        public string Id { get; set; }
        public string Value { get; set; }
        public bool Required { get; set; }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Enjoy.Core.UIElements
{
    public class RadioUIElement : UIElement
    {

        public RadioUIElement(
               string name,
               string text,
               RadioItem[] items,
               string[] linked,
               string value = null,
               bool required = false)
            : base(name, text, required, value, null)
        {
            this.Items = items;
            this.Linked = linked ?? items.Select(o => o.WhenCheckedShow).ToArray();
        }
        public RadioItem[] Items { get; set; }
        public string[] Linked { get; private set; }

        public class RadioItem
        {
            public RadioItem(string id, string name, string text, string value, string whenCheckedShow = null)
            {
                this.Id = id;
                this.Name = name;
                this.Text = text;
                this.Value = value;
                this.WhenCheckedShow = whenCheckedShow ?? string.Format("group_{0}", value);
            }
            public string Id { get; set; }
            public string Name { get; set; }
            public string Text { get; set; }
            public string Value { get; set; }
            public string WhenCheckedShow { get; set; }
        }
    }
}
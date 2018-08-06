using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Enjoy.Core.UIElements
{
    public class RadioUIElement : UIElement<string>
    {

        public RadioUIElement(
               string name,
               string value,
               RadioItem[] items,
               string[] linked = null)
            : base(name, null, value)
        {
            this.Items = items;
            this.Linked = linked ?? items.Select(o => o.WhenCheckedShow).ToArray();
            if (items.Any(o => o.Value.Equals(value)) == false)
                throw new TypeInitializationException(typeof(RadioUIElement).FullName, new IndexOutOfRangeException("value must included in radio items"));
        }
        public RadioItem[] Items { get; set; }
        public string[] Linked { get; private set; }

        public class RadioItem
        {
            public RadioItem(
                string id,
                string name,
                string text,
                string value,
                string whenCheckedShow = null,
                bool disabled = false)
            {
                this.Id = id;
                this.Name = name;
                this.Text = text;
                this.Value = value;
                this.Disabled = disabled;
                this.WhenCheckedShow = whenCheckedShow ?? string.Format("group_{0}", value);
            }
            public string Id { get; set; }
            public string Name { get; set; }
            public string Text { get; set; }
            public bool Disabled { get; set; }
            public string Value { get; set; }
            public string WhenCheckedShow { get; set; }
        }
    }
}
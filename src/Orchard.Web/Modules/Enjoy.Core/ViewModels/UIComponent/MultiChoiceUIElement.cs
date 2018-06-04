using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Enjoy.Core.UIElements
{
    public class MultiChoiceUIElement : UIElement
    {
        public MultiChoiceUIElement(string name, string text, ChoiceItem[] items)
            : base(name, text, false, "", "")
        {
            this.Items = items;
        }
        public ChoiceItem[] Items { get; set; }

        public class ChoiceItem
        {
            public ChoiceItem(string name, string value, string text, bool @checked)
            {
                this.Name = name;
                this.Checked = @checked;
                this.Text = text;
                this.Value = value;
            }
            public string Text { get; set; }
            public string Name { get; set; }
            public string Value { get; set; }
            public bool Checked { get; set; }
        }
    }

}
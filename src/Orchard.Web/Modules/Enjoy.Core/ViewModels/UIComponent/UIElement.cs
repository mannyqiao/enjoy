using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Enjoy.Core.UIElements
{
    public abstract class UIElement
    {

        public UIElement(
            string name,
            string text,
            bool required = false,
            string value = null,
            string message = null)
        {
            this.Name = name;
            this.Text = text;
            this.Value = value;
            this.Required = required;
            this.Message = message;
        }
        public string Id
        {
            get
            {
                return this.Name.Replace(".", "_");
            }
        }
        public string Name { get; set; }
        public string Text { get; protected set; }
        public string Value { get; protected set; }
        public string Message { get; private set; }
        public bool Required { get; protected set; }
    }
}
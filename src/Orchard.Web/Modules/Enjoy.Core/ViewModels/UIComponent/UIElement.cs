using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Enjoy.Core.UIElements
{
    public class UIElement<TModel, TProperty> : ISegmentOption
    {
        private readonly Expression<Func<TModel, TProperty>> Expression;
        public UIElement(
            UIType type,
            string text,
            Expression<Func<TModel, TProperty>> expression = null,
            UIElement<TModel, TProperty>[] children = null,
            string value = null,
            bool required = false)
        {
            this.Expression = expression;
            this.Text = text;
            this.Value = value;
            this.Required = required;
            this.Children = children;
        }
        public virtual UIType Type { get; protected set; }
        public string Text { get; protected set; }
        public string Value { get; protected set; }
        public bool Required { get; protected set; }
        public UIElement<TModel, TProperty>[] Children { get; protected set; }
        public virtual string IdOf()
        {
            if (this.Expression == null) throw new NullReferenceException(nameof(this.Expression));
            return UIElementHelper.IdOf(this.Expression);
        }    

        public virtual string NameOf()
        {
            if (this.Expression == null) throw new NullReferenceException(nameof(this.Expression));
            return UIElementHelper.NameOf(this.Expression);
        }

      
    }
}
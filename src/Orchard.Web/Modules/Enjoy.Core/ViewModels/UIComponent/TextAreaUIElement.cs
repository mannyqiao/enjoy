//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace Enjoy.Core.UIElements
//{
//    public class TextAreaUIElement<TModel, TProperty> : UIElement
//    {
//        private readonly Expression<Func<TModel, TProperty>> Expression;
//        public TextAreaUIElement(
//         string text,
//         string value = null,
//         string placeholder = null,
//         bool required = false)
//         : base(text, value, required)
//        {
//            this.Placeholder = placeholder;
//        }
//        public string Placeholder { get; set; }
//        public override UIType Type { get { return UIType.TextArea; } }
//        public override string IdOf()
//        {
//            return UIElementHelper.IdOf(this.Expression);
//        }

//        public override string NameOf()
//        {
//            return UIElementHelper.NameOf(this.Expression);
//        }
//    }
//}
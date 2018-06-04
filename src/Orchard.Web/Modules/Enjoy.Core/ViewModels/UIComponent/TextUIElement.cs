

//using System;
//using System.Linq.Expressions;

//namespace Enjoy.Core.UIElements
//{
//    public class TextUIElement<TModel, TProperty> : UIElement
//    {
//        private readonly Expression<Func<TModel, TProperty>> Expression;
//        public TextUIElement(
//            string text,
//            Expression<Func<TModel, TProperty>> expression,
//            string value = null,
//            string placeholder = null,
//            bool required = false)
//            : base(text, value, required)
//        {
//            this.Placeholder = placeholder;
//            this.Expression = expression;
//        }
//        public override UIType Type
//        {
//            get
//            {
//                return UIType.Text;
//            }
//        }
//        public string Placeholder { get; set; }
      
//    }
//}
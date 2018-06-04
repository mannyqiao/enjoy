//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Web;

//namespace Enjoy.Core.UIElements
//{
//    public class RadioUIElement<TModel, TProperty> : UIElement
//    {
//        private readonly Expression<Func<TModel, TProperty>> Expression;
//        public RadioUIElement(
//            RadioItem[] items,
//            string[] gearElements,
//            string id,
//            string value)
//            : base(id, string.Empty)
//        {
//            this.Items = items;
//            this.GearElements = gearElements;
//            this.Value = value;
//        }
//        public override UIType Type { get { return UIType.Radio; } }
//        public RadioItem[] Items { get; set; }
//        public string[] GearElements { get; set; }

//        public class RadioItem : ISegmentOption
//        {
//            public RadioItem(string id, string text, string value, string whenCheckedShow)
//            {
//                this.Id = id;
//                this.Text = text;
//                this.Value = value;
//                this.WhenCheckedShow = whenCheckedShow;
//            }
//            public string Id { get; set; }
//            public string Text { get; set; }
//            public string Value { get; set; }
//            public string WhenCheckedShow { get; set; }
//        }
//    }
//}
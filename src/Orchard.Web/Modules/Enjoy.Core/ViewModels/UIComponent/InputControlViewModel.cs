//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace Enjoy.Core.ViewModels
//{
  

//    public class InputControlViewModel : ControlViewModel
//    {
//        public InputControlViewModel(string name, string label,
//            string placeholder = "",
//            string value = "",
//            bool required = false)
//            : base(name, label, placeholder, value, required)
//        {

//        }
//    }
//    public class RadioItemsControlViewModel
//    {
//        public RadioItemsControlViewModel(RadioItem[] items, string[] gearElements,string name, string value)
//        {
//            this.Items = items;
//            this.GearElements = gearElements;
//            this.Name = name;
//            this.Value = value;
//        }
//        public RadioItem[] Items { get; set; }
//        public string[] GearElements { get; set; }
//        public string Value { get; set; }
//        public string Name { get; set; }
//        public class RadioItem
//        {

//            public string Name { get; set; }
//            public string Label { get; set; }
//            public string Value { get; set; }
//            public string WhenCheckedShow { get; set; }
//        }
//    }
//}
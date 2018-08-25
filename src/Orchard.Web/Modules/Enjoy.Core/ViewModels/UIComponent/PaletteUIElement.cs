
namespace Enjoy.Core.UIElements
{
    using System.Collections.Generic;
    public class PaletteUIElement : UIElement<string>
    {
        public PaletteUIElement(string name, string text, string color)
            : base(name, text, color)
        {
            this.ColorName = color;
        }
        public string RGB
        {
            get
            {
                return Constants.CouponBackgroundColors[this.Value];
            }
        }
        public string ColorName { get; set; }
        public Dictionary<string, string> Colors
        {
            get
            {
                return Constants.CouponBackgroundColors;
            }
        }

    }
}
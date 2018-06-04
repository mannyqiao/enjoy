
namespace Enjoy.Core.UIElements
{
    using System.Collections.Generic;
    public class PaletteUIElement : UIElement
    {
        public PaletteUIElement(string name, string text, string color)
            : base(name, text, false, color, "")
        {

        }
        public Dictionary<string, string> Colors
        {
            get
            {
                return EnjoyConstant.CouponBackgroundColors;
            }
        }

    }
}
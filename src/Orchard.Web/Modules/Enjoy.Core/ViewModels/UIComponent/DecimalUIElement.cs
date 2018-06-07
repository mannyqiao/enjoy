



namespace Enjoy.Core.UIElements
{
    public class DecimalUIElement : UIElement<decimal?>
    {
        public DecimalUIElement(string name,
            string text,
            decimal? value = null,
            bool required = false,
            string placeholder = null,
            string message = null)
            : base(name, text, value, required, message)
        {

        }
        
    }
}
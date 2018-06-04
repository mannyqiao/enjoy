

namespace Enjoy.Core.UIElements
{
    public class DatePickerUIElement : UIElement
    {
        private const string DATA_LINK_FORMAT_DATE = "yyyy-MM-dd";
        private const string DATA_LINK_FORMAT_TIME = "hh:ii";

        public DatePickerUIElement(
            string name,
            string text,
            string value = null,
            string format = null,
            bool required = false,
            string message = null)
            : base(name, text, required, value, message)
        {
            this.Format = format ?? DATA_LINK_FORMAT_DATE;
        }
        public string Format { get; private set; }
    }
}
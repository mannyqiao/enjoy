using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Enjoy.Core.UIElements
{
    public class ImageUploadUIElement : UIElement
    {
        public ImageUploadUIElement(
           string name,
           string text,
           MediaUploadTypes type,
           string value = null,
           string imageUrl = null,
           string message = null,
           bool required = false)
           : base(name, text, required, value, message)
        {
            this.MediaUploadTypes = type;
        }
        public string ImageUrl { get; set; }
        public MediaUploadTypes MediaUploadTypes { get; set; }
    }
}
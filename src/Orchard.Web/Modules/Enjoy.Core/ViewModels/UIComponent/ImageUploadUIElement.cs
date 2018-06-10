using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Enjoy.Core.UIElements
{
    public class ImageUploadUIElement : UIElement<string>
    {
        public ImageUploadUIElement(
           string name,
           string text,
           MediaUploadTypes type,           
           string value = null,
           bool required = false,
           string imageUrl = null,
           string message = null)
           : base(name, text, value, required, message)
        {
            this.MediaUploadTypes = type;
            this.Value = value;
            this.ImageUrl = imageUrl;            
        }
        public string ImageUrl { get; set; }
        public MediaUploadTypes MediaUploadTypes { get; set; }
    }
}
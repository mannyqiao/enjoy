using Orchard.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Enjoy.Core
{
    public static class StringExtension
    {
        public static string ToBase64(this string value)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
        }

        public static string FromBase64(this string value)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(value));
        }
        public static string GetString(this byte[] buffers, Encoding encodeing = null)
        {
            if (encodeing == null)
                encodeing = UTF8Encoding.Default;
            return encodeing.GetString(buffers);
        }
        public static byte[] FromBase64String(this string text)
        {
            return Convert.FromBase64String(text);
        }
        public static string Cleartext(this IEncryptionService service, string ciphertext)
        {
            return service.Decode(ciphertext.FromBase64String()).GetString();
        }
        public static string Ciphertext(this IEncryptionService service, string cleartext, Encoding encodeing = null)
        {
            if (encodeing == null)
                encodeing = UTF8Encoding.Default;
            return Convert.ToBase64String(service.Encode(encodeing.GetBytes(cleartext)));
        }

        public static string RepairXmlText(this string xmlMsg)
        {
            if (string.IsNullOrEmpty(xmlMsg)) return xmlMsg;
            if (xmlMsg.Trim().StartsWith("<?xml"))
            {
                return xmlMsg;
            }
            else
            {
                return string.Concat(@"<?xml version=""1.0""?>", xmlMsg);
            }
        }
    }
}
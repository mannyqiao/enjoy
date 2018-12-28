

namespace Enjoy.Core
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using Orchard.Security;
    using System.Linq;
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
            return xmlMsg;
            //if (string.IsNullOrEmpty(xmlMsg)) return xmlMsg;
            //if (xmlMsg.Trim().StartsWith("<?xml"))
            //{
            //    return xmlMsg;
            //}
            //else
            //{
            //    return string.Concat(@"<?xml version=""1.0""?>\r\n", xmlMsg);
            //}
        }
        public static string GetSHA1Crypto(this string text)
        {
            var bytes = SHA1.Create().ComputeHash(UTF8Encoding.Default.GetBytes(text));
            return bytes.ToHexString();
        }
        public static string ToHexString(this byte[] bytes)
        {
            return string.Join(string.Empty, bytes.Select(o => o.ToString("X2").ToLower()));
        }

    }
}
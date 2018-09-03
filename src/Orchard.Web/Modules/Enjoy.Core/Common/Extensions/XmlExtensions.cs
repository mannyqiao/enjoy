

namespace Enjoy.Core
{
    using System.Xml;
    using System.Xml.Serialization;
    using System.IO;
    using System.Text;
    public static class XmlExtensions
    {
        public static T DeserializeFromXml<T>(this string document)
            where T : class
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (var stream = new MemoryStream(UTF8Encoding.UTF8.GetBytes(document), false))
            {
                return serializer.Deserialize(stream) as T;
            }
        }
    }
}
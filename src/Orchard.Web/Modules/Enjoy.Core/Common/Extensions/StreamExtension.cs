using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;

namespace Enjoy.Core
{
    using System.IO;
    public static class StreamExtension
    {
        public static string ReadStream(this Stream stream)
        {
            if (null == stream)
            {
                return string.Empty;
            }
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
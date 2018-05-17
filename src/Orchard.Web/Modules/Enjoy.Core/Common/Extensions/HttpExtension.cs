

namespace Enjoy.Core
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Web;
    public static class HttpExtension
    {
        public static readonly Encoding DEFAULT_ENCODING = Encoding.UTF8;

        public static string GetUriContentDirectly(
            this string uri,
            Func<HttpWebRequest, HttpWebRequest> funcSetCustomSettings = null,
            Encoding defaultEncoding = null)
        {
            var request = WebRequest.CreateHttp(uri);
            if (funcSetCustomSettings != null)
            {
                request = funcSetCustomSettings(request);
            }

            var response = request.GetResponse() as HttpWebResponse;
            //if ((response != null) && (response.StatusCode == HttpStatusCode.OK))
            {
                var encoding = defaultEncoding;
                if (encoding == null)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(response.ContentEncoding))
                        {
                            encoding = DEFAULT_ENCODING;
                        }
                        else
                        {
                            encoding = Encoding.GetEncoding(response.ContentEncoding);
                        }
                    }
                    catch { encoding = DEFAULT_ENCODING; }
                }

                using (var reader = new StreamReader(response.GetResponseStream(), encoding))
                {
                    string context = reader.ReadToEnd();
                    return context;
                }
            }
        }
        public static string GetResponse(
            this string uri,
            Func<HttpWebRequest, HttpWebRequest> funcSetCustomSettings = null,
            Encoding defaultEncoding = null,
            ITransientFaultDetecter<Exception> faultDetecter = null,
            int retryThreshold = 5)
        {
            var faultHandler = new DefaultTransientFaultHandler<string>(
                () => { return GetUriContentDirectly(uri, funcSetCustomSettings, defaultEncoding); },
                faultDetecter ?? new DefaultHttpTransientFaultDetecter(),
                retryThreshold);
            return faultHandler.Execute();
        }

        public static T GetResponse<T>(
            this string uri,
            Func<string, T> funcDeserialize,
            Func<HttpWebRequest, HttpWebRequest> funcSetCustomSettings = null,
            Encoding defaultEncoding = null,
            ITransientFaultDetecter<Exception> faultDetecter = null,
            int retryThreshold = 5)
        {
            return funcDeserialize(uri.GetResponse(
                funcSetCustomSettings,
                defaultEncoding,
                faultDetecter,
                retryThreshold));
        }

        public static T GetResponseForJson<T>(
            this string uri,
            Func<HttpWebRequest, HttpWebRequest> funcSetCustomSettings = null,
            Encoding defaultEncoding = null,
            ITransientFaultDetecter<Exception> faultDetecter = null,
            int retryThreshold = 5)
        {
            return uri.GetResponse(
                JsonExtension.DeserializeToObject<T>,
                funcSetCustomSettings,
                defaultEncoding,
                faultDetecter,
                retryThreshold);
        }

        public static string UrlEncode(this string source)
        {
            return HttpUtility.UrlEncode(source);
        }
        
        public static string UrlDecode(this string source)
        {
            return HttpUtility.UrlDecode(source);
        }

    }
}

using Orchard;
using Orchard.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EnjoyTheme.Filters
{
    public class LayoutFilter : FilterProvider, IResultFilter
    {

        static string[] LayoutLogOnUrls = new string[]
        {
            "/Access/Sign",
            "/Access/SignUp"
        };
        static string[] LayoutDashboardUrls = new string[]
        {
              "/Dashboard/Summary",              
        };

        private readonly IWorkContextAccessor _wca;
        public LayoutFilter(IWorkContextAccessor wca)
        {
            _wca = wca;
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var WorkContext = _wca.GetContext();
            if (LayoutLogOnUrls.Any(o => WorkContext.HttpContext.Request.Url.PathAndQuery.IndexOf(o, StringComparison.OrdinalIgnoreCase) >= 0))
            {
                WorkContext.Layout.Metadata.Alternates.Add("Layout__Sign");
            }
            if (LayoutDashboardUrls.Any(o => WorkContext.HttpContext.Request.Url.PathAndQuery.IndexOf(o, StringComparison.OrdinalIgnoreCase) >= 0))
            {
                WorkContext.Layout.Metadata.Alternates.Add("Layout__Dashboard");
            }
            //if (LayoutOnTaskMan.Any(o => workContext.HttpContext.Request.Url.PathAndQuery.IndexOf(o, StringComparison.OrdinalIgnoreCase) >= 0))
            //{
            //    WorkContext.Layout.Metadata.Alternates.Add("Layout__LogOnTaskMan");
            //}
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
        }

        private static string BuildShapeName(
            RouteValueDictionary values, params string[] names)
        {
            var layout = "Layout__" +
                string.Join("__",
                    names.Select(s =>
                        ((string)values[s] ?? "").Replace(".", "_")));
            return layout;
        }
    }
}
using Orchard.Mvc.Routes;


namespace Enjoy.Core
{
    using Autofac;
    using Newtonsoft.Json.Serialization;
    using Orchard.WebApi.Routes;
    using System.Collections.Generic;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using System.Web.Http;
    
    using System.Linq;
    public class ApiRoutes: IHttpRouteProvider
    {
        public IEnumerable<RouteDescriptor> GetRoutes()
        {
            return new RouteDescriptor[] {
                new HttpRouteDescriptor {
                    Name = "Default Api",
                    Priority = 0,
                    RouteTemplate =  "api/{controller}/{action}/{id}",
                    Defaults = new {
                        area = "Enjoy.Core",
                        controller = "Enjoy",
                        id = RouteParameter.Optional
                    }
                }
            };
        }

        public void GetRoutes(ICollection<RouteDescriptor> routes)
        {
            foreach (RouteDescriptor routeDescriptor in GetRoutes())
            {
                routes.Add(routeDescriptor);
            }
        }
    }

    public class WebApiConfig: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
            var jsonFormatter = GlobalConfiguration.Configuration.Formatters.OfType<JsonMediaTypeFormatter>().FirstOrDefault();

            if (jsonFormatter != null)
            {
                jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }
        }
    }
}
using Orchard.Mvc.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Enjoy.Core
{
    public class Routes : IRouteProvider
    {
        public void GetRoutes(ICollection<RouteDescriptor> routes)
        {
            foreach (var routeDescriptor in GetRoutes())
                routes.Add(routeDescriptor);
        }

        public IEnumerable<RouteDescriptor> GetRoutes()
        {
            return new[] {
                new RouteDescriptor {
                    Route = new Route(
                        "{controller}/{action}",
                        new RouteValueDictionary {
                            {"area", "Enjoy.Core"},
                            {"controller", "Dashboard"},
                            {"action", "Summary"}
                        },
                        new RouteValueDictionary {
                            {"area", "Enjoy.Core"},
                            {"controller", "Dashboard"},
                        },
                        new RouteValueDictionary {
                            {"area", "Enjoy.Core"}
                        },
                        new MvcRouteHandler())
                }
            };
        }
    }
}
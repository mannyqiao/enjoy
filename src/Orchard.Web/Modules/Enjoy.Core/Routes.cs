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
                CreateRouteDescriptor("Enjoy.Core","Dashboard","Summary"),
                CreateRouteDescriptor("Enjoy.Core","Access","Sign"),
                CreateRouteDescriptor("Enjoy.Core","Access","Signin"),
                CreateRouteDescriptor("Enjoy.Core","Access","GetverificationCode"),
                CreateRouteDescriptor("Enjoy.Core","Merchant","Create"),
                CreateRouteDescriptor("Enjoy.Core","Merchant","View"),
                CreateRouteDescriptor("Enjoy.Core","Merchant","UploadMaterial"),
                CreateRouteDescriptor("Enjoy.Core","Merchant","Shops"),
                CreateRouteDescriptor("Enjoy.Core","Merchant","Audit"),
                CreateRouteDescriptor("Enjoy.Core","Merchant","MyMerchant"),
                CreateRouteDescriptor("Enjoy.Core","Merchant","QueryMyShops"),
                CreateRouteDescriptor("Enjoy.Core","Merchant","EditShop"),
                CreateRouteDescriptor("Enjoy.Core","Merchant","EditShopPost"),

                CreateRouteDescriptor("Enjoy.Core","Finance","PAccount"),
                CreateRouteDescriptor("Enjoy.Core","Finance","MyAccount"),

                CreateRouteDescriptor("Enjoy.Core","Cards","Coupon"),
                CreateRouteDescriptor("Enjoy.Core","Cards","MCard"),
                CreateRouteDescriptor("Enjoy.Core","Cards","Edit"),
                CreateRouteDescriptor("Enjoy.Core","Cards","EditPost"),
                CreateRouteDescriptor("Enjoy.Core","Cards","Publish"),
                CreateRouteDescriptor("Enjoy.Core","Cards","ShowQR"),
                CreateRouteDescriptor("Enjoy.Core","Cards","QueryCouponCard")
                

                //new RouteDescriptor {
                //    Route = new Route(
                //        "{controller}/{action}",
                //        new RouteValueDictionary {
                //            {"area", "Enjoy.Core"},
                //            {"controller", "Dashboard"},
                //            {"action", "Summary"}
                //        },
                //        new RouteValueDictionary {
                //            {"area", "Enjoy.Core"},
                //            {"controller", "Dashboard"},
                //        },
                //        new RouteValueDictionary {
                //            {"area", "Enjoy.Core"}
                //        },
                //        new MvcRouteHandler())
                //}
            };
        }
        static RouteDescriptor CreateRouteDescriptor(string area, string controller, string action)
        {
            return new RouteDescriptor
            {
                Route = new Route(
                        "{controller}/{action}",
                        new RouteValueDictionary {
                            {"area", area},
                            {"controller", controller},
                            {"action", action}
                        },
                        new RouteValueDictionary {
                            {"area", area},
                            {"controller",controller},
                        },
                        new RouteValueDictionary {
                            {"area",area}
                        },
                        new MvcRouteHandler())
            };
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KuroganeHammer.Web
{
    public class RouteConfig
    {
        public static void RegisterAPIROUTE(RouteCollection APIROUTE)
        {
            APIROUTE.IgnoreRoute("{resource}.axd/{*pathInfo}");

            APIROUTE.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace LectioServer.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("index.html");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.#1#)?favicon.ico(/.*)?" });

            routes.MapRoute(
                name: "Default",
                url: "admin/{controller}/{action}/{id}",
                defaults: new { action = "index", id = UrlParameter.Optional }
            );
        }
    }
}
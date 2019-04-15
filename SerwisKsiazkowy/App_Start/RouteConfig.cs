using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SerwisKsiazkowy
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "{title}",
                url: "ksiazka/{title}-id-{id}.html", //{id}
                defaults: new { controller = "Book", action = "Details" }
            );

            routes.MapRoute(
                name: "StaticPages",
                url: "strona/{viewname}.html",
                defaults: new { controller = "Home", action = "StaticContent" }
            );

            routes.MapRoute(
                name: "BookList",
                url: "gatunek/{genrename}",
                defaults: new { controller = "Book", action = "ListGenres" },
                constraints: new { genrename = @"[\w]+"}
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

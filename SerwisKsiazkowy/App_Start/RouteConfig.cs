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
                name: "Ksiazka",
                url: "ksiazka/{_title}-i{id}", //{id}
                defaults: new { controller = "Book", action = "Details"  }
            );
            routes.MapRoute(
                name: "Ksiazka1",
                url: "ksiazka/{bookTitle}-i{bookId}", //{id}
                defaults: new { controller = "Book", action = "Details" }
            );
            routes.MapRoute(
                name: "Recenzja",
                url: "recenzja/{bookTitle}-i{bookId}",
                defaults: new { controller = "Review", action = "Index" }
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
                name: "Filterlist",
                url: "gatunek/{genrename}/filter",
                defaults: new { controller = "Book", action = "Filterlist" },
                constraints: new { genrename = @"[\w]+" }
            );

            routes.MapRoute(
                name: "Komentarze",
                url: "ksiazka/{_title}-i{id}/komentarze", //{id}
                defaults: new { controller = "Comment", action = "ListComments" }
            );
            routes.MapRoute(
               name: "Recenzje",
               url: "ksiazka/{_title}-i{id}/recenzje", //{id}
               defaults: new { controller = "Review", action = "ListReviews" }
           );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            
        }
    }
}

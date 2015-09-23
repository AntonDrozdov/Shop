using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCGUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(null,
                           "Admin/Goods",
                           new { controller = "Admin", action = "GoodsList"}
                           );
            routes.MapRoute(null,
               "Admin/CategoryList/Page{page}",
               new { controller = "Admin", action = "CategoriesList" }
               );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Admin", action = "GoodsList", id = UrlParameter.Optional }
            );
        }
    }
}
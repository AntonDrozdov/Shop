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


            routes.MapRoute(null,"Admin/Goods/Page{page}",new { controller = "Admin", action = "GoodsList", SelectedSection = "Goods"});

            routes.MapRoute(null, "Admin/CategoryList", new { controller = "Admin", action = "CategoriesList", SelectedSection = "Categories" });
            routes.MapRoute(null,"Admin/CategoryList/Page{page}",new { controller = "Admin", action = "CategoriesList", SelectedSection = "Categories" });
            
            routes.MapRoute(null,"Admin/CategoryTypesList",new { controller = "Admin", action = "CategoryTypesList", SelectedSection = "CategoryTypes" });
           
            routes.MapRoute(null,"Admin/PurchasesList",new { controller = "Admin", action = "PurchasesList", SelectedSection = "Purchases" });
            routes.MapRoute(null,"Admin/PurchasesList/Page{page}",new { controller = "Admin", action = "PurchasesList", SelectedSection = "Purchases" });
            
            routes.MapRoute(null,"Admin/SalesList",new { controller = "Admin", action = "SalesList", SelectedSection = "Sales" });
            routes.MapRoute(null,"Admin/SalesList/Page{page}",new { controller = "Admin", action = "SalesList", SelectedSection = "Sales" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Admin", action = "GoodsList", id = UrlParameter.Optional }
            );
        }
    }
}
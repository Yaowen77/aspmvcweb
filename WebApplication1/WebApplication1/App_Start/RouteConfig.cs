using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication1
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");



            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            //);


            //routes.MapRoute(
            //    name: "Error",
            //    url: "Error/{code}",
            //    defaults: new { controller = "Login", action = "Index", code = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Default",   // 自行命名。
                url: "{controller}/{action}/{id}",    // 預設值 id。
                                                                                             
                defaults: new { controller = "Login", action = "Index2", id = UrlParameter.Optional } 
            );


        }
    }
}

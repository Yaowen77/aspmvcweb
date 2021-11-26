using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace WebApplication1
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }


        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {   
           
            if (HttpContext.Current.User == null) return;   
            if (HttpContext.Current.User.Identity.IsAuthenticated == false) return;
            if (Request.IsAuthenticated == false) return;


            FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;

            FormsAuthenticationTicket authTicket = id.Ticket;

            string[] arrRoles = authTicket.UserData.Split(',');

            HttpContext.Current.User = new GenericPrincipal(HttpContext.Current.User.Identity, arrRoles);

        }
    }
}

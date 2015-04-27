using DemoImpersonateWindowsUserAspnet.Models;
using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DemoImpersonateWindowsUserAspnet
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

#if DEBUG // For security/audit reasons, you should not give the ability to impersonate a user on Production
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            if (HttpContext.Current.Request.Cookies[Constants.ImpersonateTrickCookieName] != null)
            {
                string userName = HttpContext.Current.Request.Cookies[Constants.ImpersonateTrickCookieName].Value;
                if (userName != null)
                {
                    HttpContext.Current.User = new WindowsPrincipal(new  System.Security.Principal.WindowsIdentity(userName, HttpContext.Current.User.Identity.AuthenticationType));
                }
            }
        }
#endif

    }
}

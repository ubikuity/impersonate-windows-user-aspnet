using DemoImpersonateWindowsUserAspnet.Models;
using System;
using System.Web;
using System.Web.Mvc;

namespace DemoImpersonateWindowsUserAspnet.Controllers
{
#if DEBUG // For security/audit reasons, you should not give the ability to impersonate a user on Production
    public class LogAsController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var userNameWithoutDomain = Request.QueryString["user"];
            if (!string.IsNullOrEmpty(userNameWithoutDomain) && !userNameWithoutDomain.Contains(@"\"))
            {
                var userNameCookie = new HttpCookie(Constants.ImpersonateTrickCookieName) { Domain = null, Value = userNameWithoutDomain, Path = Request.ApplicationPath, HttpOnly = false };
                HttpContext.Response.Cookies.Add(userNameCookie);
            }
            else
            {
                // Delete cookie and go back real connected user
                DeleteImpersonationCookie();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Reset()
        {
            DeleteImpersonationCookie();
            return RedirectToAction("Index", "Home");
        }

        private void DeleteImpersonationCookie()
        {
            if (HttpContext.Response.Cookies[Constants.ImpersonateTrickCookieName] != null)
            {
                HttpContext.Response.Cookies[Constants.ImpersonateTrickCookieName].Value = null;
                HttpContext.Response.Cookies[Constants.ImpersonateTrickCookieName].Expires = DateTime.Now.AddMonths(-1);
            }
        }
    }
#endif
}
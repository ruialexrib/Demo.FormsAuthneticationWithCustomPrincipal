using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly AuthenticationService _authenticationService;

        public HomeController()
        {
            _authenticationService = new AuthenticationService();
        }

        [Authorize]
        public ActionResult Index()
        {
            var user = User as ClassLibrary1.CustomPrincipal;
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public void Logoff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            // clear authentication cookie
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);

            // clear session cookie (not necessary for your current problem but i would recommend you do it anyway)
            SessionStateSection sessionStateSection = (SessionStateSection)WebConfigurationManager.GetSection("system.web/sessionState");
            HttpCookie cookie2 = new HttpCookie(sessionStateSection.CookieName, "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);

            FormsAuthentication.RedirectToLoginPage();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                HttpCookie faCookie = _authenticationService.Authenticate(user);
                Response.Cookies.Add(faCookie);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "invalid Username or Password");
            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
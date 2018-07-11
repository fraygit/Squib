using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Squib.Frontend.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Signout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Index(string email, string password)
        {
            var isUserValid = Membership.ValidateUser(email, password);
            if (isUserValid)
            {
                FormsAuthentication.SetAuthCookie(email, false);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Message = "Invalid username or password";

            return View();
        }
    }
}
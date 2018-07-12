using MongoDB.Bson;
using Squib.Data.Interface;
using Squib.Data.Model;
using Squib.Data.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Squib.Frontend.Controllers
{
    public class LoginController : Controller
    {
        private IUserRepository _userRepository;

        public LoginController(IUserRepository _userRepository)
        {
            this._userRepository = _userRepository;
        }

        // GET: Login
        public ActionResult Index()
        {
            if (Request.QueryString["ReturnUrl"] != null)
            {
                ViewBag.ReturnUrl = Request.QueryString["ReturnUrl"];
            }
            return View();
        }

        public ActionResult Signout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(string email, string password, string passwordRepeat, string firstName, string lastName)
        {
            if (password != passwordRepeat)
            {
                ViewBag.Message = "Password does not match";
                return View();
            }
            if (!email.Contains("@") || !email.Contains("."))
            {
                ViewBag.Message = "Please enter a valid email address";
                return View();
            }
            var findExistingEmail = await _userRepository.GetUser(email);
            if (findExistingEmail == null)
            {

                var user = new User
                {
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    VerificationCode = ObjectId.GenerateNewId().ToString(),
                    Password = SquibCrypto.HashSha256(password)
                };
                await _userRepository.CreateSync(user);

                FormsAuthentication.SetAuthCookie(email, false);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Message = "Email with the same user already exists.";
            return View();
        }

        [HttpPost]
        public ActionResult Index(string email, string password, string returnUrl)
        {
            var isUserValid = Membership.ValidateUser(email, password);
            if (isUserValid)
            {
                FormsAuthentication.SetAuthCookie(email, false);
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    Response.Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Message = "Invalid username or password";

            return View();
        }
    }
}
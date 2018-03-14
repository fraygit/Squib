using Squib.Backoffice.Models;
using Squib.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Squib.Backoffice.Controllers
{
    public class LoginController : Controller
    {
        private IUserRepository _userRepository;

        public LoginController(IUserRepository _userRepository)
        {
            this._userRepository = _userRepository;
        }

        public ActionResult Index(ReqLogin request)
        {
            return View(request);
        }

        public async Task<ActionResult> Validate(ReqLogin loginModel)
        {
            ViewBag.ErrorMessage = "";
            var existingUser = await _userRepository.GetUser(loginModel.Username);
            if (existingUser != null)
            {
                if (existingUser.Status == "Registered")
                {
                    ViewBag.ErrorMessage = "Account not yet activated.";
                    return RedirectToAction("Index", new LoginModel { Message = "Email not verified." });
                }
            }

            var isUserValid = Membership.ValidateUser(loginModel.Username, loginModel.Password);
            if (isUserValid)
            {
                FormsAuthentication.SetAuthCookie(loginModel.Username, false);
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return RedirectToAction("Index", new LoginModel { Message = "Invalid username or password." });
            }
        }

    }
}

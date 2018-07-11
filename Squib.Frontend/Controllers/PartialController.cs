using Squib.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Squib.Frontend.Controllers
{
    public class PartialController : Controller
    {
        private IUserRepository _userRepository;

        public PartialController(IUserRepository _userRepository)
        {
            this._userRepository = _userRepository;
        }

        // GET: Partial
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Header()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = Task.Run(() => _userRepository.GetUser(User.Identity.Name)).Result;
                ViewBag.Name = user.FirstName;
            }
            return PartialView();
        }
    }
}
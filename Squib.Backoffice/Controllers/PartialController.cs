using Squib.Backoffice.Models;
using Squib.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Squib.Backoffice.Controllers
{
    public class PartialController : Controller
    {
        //
        // GET: /Partial/

        private IUserRepository _userRepository;

        public PartialController(IUserRepository _userRepository)
        {
            this._userRepository = _userRepository;
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Header()
        {
            return PartialView();
        }

        public ActionResult Navigation()
        {
            var user = Task.Run(() => _userRepository.GetUser(User.Identity.Name)).Result;
            var model = new ResNavigation();
            model.User = user;
            return PartialView(model);
        }

    }
}

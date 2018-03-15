using Squib.Backoffice.Models;
using Squib.Data.Interface;
using Squib.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Squib.Backoffice.Controllers
{
    public class PromoController : Controller
    {
        private IUserRepository _userRepository;
        private IPromoRepository _promoRepository;

        public PromoController(IUserRepository _userRepository)
        {
            this._userRepository = _userRepository;
            this._promoRepository = _promoRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<JsonResult> SaveNew(Promo request)
        {

            return Json(new JsonGenericResult
            {
                IsSuccess = false,
                Message = "System error."
            });
        }


    }
}

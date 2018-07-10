using Squib.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Squib.Frontend.Controllers
{
    public class PromoController : Controller
    {
        private IUserRepository _userRepository;
        private IPromoRepository _promoRepository;
        private IOrganisationRepository _organisationRepository;

        public PromoController(IUserRepository _userRepository, IPromoRepository _promoRepository, IOrganisationRepository _organisationRepository)
        {
            this._userRepository = _userRepository;
            this._promoRepository = _promoRepository;
            this._organisationRepository = _organisationRepository;
        }

        // GET: Promo
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Details(string promoId)
        {
            var promo = await _promoRepository.Get(promoId);
            return View(promo);
        }
    }
}
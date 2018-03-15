using Squib.Backoffice.Models;
using Squib.Backoffice.Services;
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
        private IOrganisationRepository _organisationRepository;

        public PromoController(IUserRepository _userRepository, IPromoRepository _promoRepository, IOrganisationRepository _organisationRepository)
        {
            this._userRepository = _userRepository;
            this._promoRepository = _promoRepository;
            this._organisationRepository = _organisationRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<ActionResult> Create()
        {
            var model = new CreatePromoViewModel();

            var orgService = new OrganisationService(_organisationRepository, _userRepository);
            model.Organisations = await orgService.Get(User.Identity.Name);

            return View(model);
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

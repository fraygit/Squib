using Squib.Data.Interface;
using Squib.Frontend.Models;
using Squib.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Squib.Frontend.Controllers
{
    public class HomeController : Controller
    {
        private IUserRepository _userRepository;
        private IPromoRepository _promoRepository;
        private IOrganisationRepository _organisationRepository;

        public HomeController(IUserRepository _userRepository, IPromoRepository _promoRepository, IOrganisationRepository _organisationRepository)
        {
            this._userRepository = _userRepository;
            this._promoRepository = _promoRepository;
            this._organisationRepository = _organisationRepository;
        }


        public async Task<ActionResult> Index()
        {
            var response = new ResponseHome();

            var promos = new List<ResPromoList>();
            var allPromos = await _promoRepository.ListAll();
            foreach (var promo in allPromos)
            {
                if (promo.Status == "Published")
                {
                    var organisation = await _organisationRepository.Get(promo.OrganisationId.ToString());
                    var statusText = promo.Status == null ? "Draft" : promo.Status;
                    promos.Add(new ResPromoList
                    {
                        Id = promo.Id,
                        Category = promo.Category,
                        Title = promo.Title,
                        Price = promo.Price,
                        From = promo.From,
                        To = promo.To,
                        OrganisationName = organisation.Name,
                        MaxNumberOfVoucher = promo.MaxNumberOfVoucher,
                        StatusText = statusText,
                        ShortDescription = promo.ShortDescription,
                        OriginalPrice = promo.OriginalPrice,
                        ImageText = promo.ImageText,
                        Images = promo.Images
                        
                    });
                }
            }

            if (promos.Any())
            {
                response.Promos = promos;
            }
            return View(response);
        }


        public async Task<ActionResult> GetFeaturedPromos()
        {
            var promos = new List<ResPromoList>();
            var allPromos = await _promoRepository.ListAll();
            foreach (var promo in allPromos)
            {
                if (promo.Status == "Published")
                {
                    var organisation = await _organisationRepository.Get(promo.OrganisationId.ToString());
                    var statusText = promo.Status == null ? "Draft" : promo.Status;
                    promos.Add(new ResPromoList
                    {
                        Id = promo.Id,
                        Category = promo.Category,
                        Title = promo.Title,
                        From = promo.From,
                        To = promo.To,
                        OrganisationName = organisation.Name,
                        MaxNumberOfVoucher = promo.MaxNumberOfVoucher,
                        StatusText = statusText
                    });
                }
            }

            return Json(promos.FirstOrDefault(), JsonRequestBehavior.AllowGet);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
using Squib.Data.Interface;
using Squib.Frontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Squib.Frontend.Controllers
{
    public class ProfileController : Controller
    {
        private IUserRepository _userRepository;
        private IPromoRepository _promoRepository;
        private IOrganisationRepository _organisationRepository;
        private IPaymentTransactionRepository _paymentTransactionRepository;
        private ICouponRepository _couponRespository;

        public ProfileController(IUserRepository _userRepository, IPromoRepository _promoRepository, IOrganisationRepository _organisationRepository, IPaymentTransactionRepository _paymentTransactionRepository, ICouponRepository _couponRespository)
        {
            this._userRepository = _userRepository;
            this._promoRepository = _promoRepository;
            this._organisationRepository = _organisationRepository;
            this._paymentTransactionRepository = _paymentTransactionRepository;
            this._couponRespository = _couponRespository;
        }
        
        [Authorize]
        public async Task<ActionResult> Index()
        {
            var coupons = await _couponRespository.GetByUser(User.Identity.Name);
            var model = new List<CouponPromoList>();
            if (coupons.Any())
            {
                foreach(var coupon in coupons.OrderByDescending(n => n.DateExpires))
                {
                    var promo = await _promoRepository.Get(coupon.PromoId.ToString());
                    model.Add(new CouponPromoList
                    {
                        Coupon = coupon,
                        Promo = promo
                    });
                }
            }
            return View(model);
        }
    }
}
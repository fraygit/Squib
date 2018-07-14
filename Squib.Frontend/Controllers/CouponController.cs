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
    public class CouponController : Controller
    {
        private IUserRepository _userRepository;
        private IPromoRepository _promoRepository;
        private IOrganisationRepository _organisationRepository;
        private IPaymentTransactionRepository _paymentTransactionRepository;
        private ICouponRepository _couponRespository;

        public CouponController(IUserRepository _userRepository, IPromoRepository _promoRepository, IOrganisationRepository _organisationRepository, IPaymentTransactionRepository _paymentTransactionRepository, ICouponRepository _couponRespository)
        {
            this._userRepository = _userRepository;
            this._promoRepository = _promoRepository;
            this._organisationRepository = _organisationRepository;
            this._paymentTransactionRepository = _paymentTransactionRepository;
            this._couponRespository = _couponRespository;
        }

        // GET: Coupon
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Redeem(string couponId)
        {
            var coupon = await _couponRespository.Get(couponId);
            var promo = await _promoRepository.Get(coupon.PromoId.ToString());

            var model = new CouponPromoList
            {
                Promo = promo,
                Coupon = coupon
            };

            return View(model);
        }
    }
}
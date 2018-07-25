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
    public class RedeemController : Controller
    {
        private IUserRepository _userRepository;
        private IPromoRepository _promoRepository;
        private IOrganisationRepository _organisationRepository;
        private ICouponRepository _couponRespository;

        public RedeemController(IUserRepository _userRepository, IPromoRepository _promoRepository, IOrganisationRepository _organisationRepository, ICouponRepository _couponRespository)
        {
            this._userRepository = _userRepository;
            this._promoRepository = _promoRepository;
            this._organisationRepository = _organisationRepository;
            this._couponRespository = _couponRespository;
        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<JsonResult> GetCoupon(string qrcode)
        {
            var response = new ResponseGetQRCode();
            var coupon = await _couponRespository.Get(qrcode);
            response.Coupon = coupon;
            if (coupon != null)
            {
                var user = await _userRepository.GetUser(coupon.User);
                response.User = user;
                var promo = await _promoRepository.Get(coupon.PromoId.ToString());
                response.Promo = promo;
            }           

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public async Task<JsonResult> Redeem(string qrcode)
        {
            var response = new ResponseGetQRCode();
            var coupon = await _couponRespository.Get(qrcode);
            response.Coupon = coupon;
            if (coupon != null)
            {
                var user = await _userRepository.GetUser(coupon.User);
                response.User = user;
                var promo = await _promoRepository.Get(coupon.PromoId.ToString());
                response.Promo = promo;
                if (coupon.Status == "Valid")
                {
                    coupon.Status = "Redeemed";
                    await _couponRespository.Update(qrcode, coupon);
                    return Json(response, JsonRequestBehavior.AllowGet);
                }
                else if (coupon.Status == "Redeemed")
                {
                    return Json(new JsonGenericResult
                    {
                        IsSuccess = false,
                        Message = "Please coupon already redeemed."
                    });
                }
            }
            return Json(new JsonGenericResult
            {
                IsSuccess = false,
                Message = "Invalid coupon."
            });

        }

    }
}

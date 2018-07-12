using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Squib.Frontend.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout
        [Authorize]
        public ActionResult Index(string promoId)
        {
            return View();
        }

    }
}
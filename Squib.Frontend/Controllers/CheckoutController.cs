using Braintree;
using MongoDB.Bson;
using Squib.Data.Interface;
using Squib.Data.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Squib.Frontend.Controllers
{
    public class CheckoutController : Controller
    {
        private IUserRepository _userRepository;
        private IPromoRepository _promoRepository;
        private IOrganisationRepository _organisationRepository;
        private IPaymentTransactionRepository _paymentTransactionRepository;
        private ICouponRepository _couponRespository;

        public CheckoutController(IUserRepository _userRepository, IPromoRepository _promoRepository, IOrganisationRepository _organisationRepository, IPaymentTransactionRepository _paymentTransactionRepository, ICouponRepository _couponRespository)
        {
            this._userRepository = _userRepository;
            this._promoRepository = _promoRepository;
            this._organisationRepository = _organisationRepository;
            this._paymentTransactionRepository = _paymentTransactionRepository;
            this._couponRespository = _couponRespository;
        }

        // GET: Checkout
        [Authorize]
        public async Task<ActionResult> Index(string promoId)
        {
            ViewBag.ShowSuccess = false;
            ViewBag.Quantity = 1;
            var promo = await _promoRepository.Get(promoId);
            ViewBag.Total = ViewBag.Quantity * promo.Price;

            var gateway = CreateBrainTreeGateway();
            var clientToken = gateway.ClientToken.Generate();
            ViewBag.ClientToken = clientToken;

            return View(promo);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Index(string promoId, string quantity)
        {
            ViewBag.ShowSuccess = false;
            ViewBag.Quantity = int.Parse(quantity);
            var promo = await _promoRepository.Get(promoId);
            ViewBag.Total = ViewBag.Quantity * promo.Price;

            var gateway = CreateBrainTreeGateway();
            var clientToken = gateway.ClientToken.Generate();
            ViewBag.ClientToken = clientToken;

            return View(promo);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> ProcessPayment()
        {
            ViewBag.ShowSuccess = false;
            var gateway = CreateBrainTreeGateway();
            Decimal amount;

            amount = Convert.ToDecimal(Request["amount"]);
            var nonce = Request["payment_method_nonce"];
            var promoId = Request["promoId"];
            var quantity = int.Parse(Request["quantity"]);

            var request = new TransactionRequest
            {
                Amount = amount,
                PaymentMethodNonce = nonce,
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }
            };
            Result<Transaction> result = gateway.Transaction.Sale(request);

            var transation = new PaymentTransaction
            {
                DateCreated = DateTime.Now,
                PaymentId = result.Target.Id,
                PromoId = ObjectId.Parse(promoId),
                Quantity = quantity,
                Status = result.Target.Status.ToString(),
                TotalPrice = amount,
                TransactionDate = DateTime.Now,
                User = User.Identity.Name
            };
            await _paymentTransactionRepository.CreateSync(transation);

            if (result.IsSuccess())
            {
                for(var i=1; i <= quantity; i++)
                {
                    await _couponRespository.CreateSync(new Coupon
                    {
                        DateCreated = DateTime.Now,
                        PromoId = ObjectId.Parse(promoId),
                        TransactionId = transation.Id,
                        User = User.Identity.Name
                    });
                }

                ViewBag.ShowSuccess = true;
            }
            else if (result.Transaction != null)
            {

            }
            else
            {

            }

            ViewBag.Quantity = quantity;
            var promo = await _promoRepository.Get(promoId);
            ViewBag.Total = ViewBag.Quantity * promo.Price;

            return View(promo);
        }

        private BraintreeGateway CreateBrainTreeGateway()
        {
            var environmentSetting = ConfigurationManager.AppSettings["BrainTreeEnvironment"].ToString();
            var environment = Braintree.Environment.SANDBOX;
            switch (environmentSetting)
            {
                case "SANDBOX":
                    environment = Braintree.Environment.SANDBOX;
                    break;
            }
            return new BraintreeGateway(environment, 
                ConfigurationManager.AppSettings["BrainTreeMerchantId"].ToString(),
                ConfigurationManager.AppSettings["BrainTreePublicKey"].ToString(),
                ConfigurationManager.AppSettings["BrainTreePrivateKey"].ToString());
        }

    }
}
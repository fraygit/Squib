using MongoDB.Bson;
using Squib.Backoffice.Models;
using Squib.Backoffice.Services;
using Squib.Data.Interface;
using Squib.Data.Model;
using System;
using System.Collections.Generic;
using System.IO;
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
        public async Task<ActionResult> Create(string id)
        {
            var model = new CreatePromoViewModel();

            if (!string.IsNullOrEmpty(id))
            {
                model.Promo = await _promoRepository.Get(id);
            }
            else
            {
                model.Promo = new Promo();
            }

            var orgService = new OrganisationService(_organisationRepository, _userRepository);
            model.Organisations = await orgService.Get(User.Identity.Name);

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<JsonResult> UploadImage()
        {
            var promoId = Request["PromoId"].ToString();
            if (Request.Files.Count > 0)
            {
                for (var i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    var filename = Path.GetFileName(file.FileName);
                    file.SaveAs(Server.MapPath("~/TempUpload/" + filename));
                }
            }
            return Json("");
        }


        [Authorize]
        [HttpPost]
        public async Task<JsonResult> Save(ReqCreatePromo request)
        {
            if (!string.IsNullOrEmpty(request.OrganisationId))
            {
                var newPromo = new Promo
                {
                    Title = request.Title,
                    Price = request.Price,
                    Category = request.Category,
                    From = request.From,
                    To = request.To,
                    Description = request.Details,
                    MaxNumberOfVoucher = request.Max,
                    OrganisationId = ObjectId.Parse(request.OrganisationId)
                };

                ObjectId promoId;

                ObjectId.TryParse(request.Id, out promoId);
                if (promoId != ObjectId.Empty)
                {
                    newPromo.Id = promoId;
                    await _promoRepository.Update(request.Id, newPromo);
                }
                else
                {
                    newPromo.IsPublished = false;
                    await _promoRepository.CreateSync(newPromo);
                }

                return Json(new JsonGenericResult
                {
                    IsSuccess = true,
                    Result = newPromo.Id.ToString()
                });

            }

            return Json(new JsonGenericResult
            {
                IsSuccess = false,
                Message = "Please select an organisation."
            });
        }


    }
}

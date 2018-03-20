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
        public async Task<ActionResult> List()
        {
            var user = await _userRepository.GetUser(User.Identity.Name);
            var promos = new List<ResPromoList>();
            foreach(var org in user.Organisations)
            {
                var organisation = await _organisationRepository.Get(org.ToString());
                var orgPromos = await _promoRepository.GetByOrg(org);
                foreach (var promo in orgPromos)
                {
                    promos.Add(new ResPromoList
                    {
                        Id = promo.Id,
                        Category = promo.Category,
                        Title = promo.Title,
                        From = promo.From,
                        To = promo.To,
                        OrganisationName = organisation.Name
                    });
                }
            }
            return View(promos);
        }

        [Authorize]
        public async Task<JsonResult> PromoList()
        {
            var user = await _userRepository.GetUser(User.Identity.Name);
            var promos = new List<ResPromoList>();
            foreach (var org in user.Organisations)
            {
                var organisation = await _organisationRepository.Get(org.ToString());
                var orgPromos = await _promoRepository.GetByOrg(org);
                foreach (var promo in orgPromos)
                {
                    promos.Add(new ResPromoList
                    {
                        Id = promo.Id,
                        Category = promo.Category,
                        Title = promo.Title,
                        From = promo.From,
                        To = promo.To,
                        OrganisationName = organisation.Name,
                        MaxNumberOfVoucher = promo.MaxNumberOfVoucher
                    });
                }
            }
            return Json(promos, JsonRequestBehavior.AllowGet);
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

        public async Task<PartialViewResult> Images(string id)
        {
            Promo model = new Promo();
            if (!string.IsNullOrEmpty(id))
            {
                model = await _promoRepository.Get(id);
            }
            return PartialView(model.Images);
        }

        [Authorize]
        [HttpPost]
        public async Task<JsonResult> UploadImage()
        {
            var promoId = Request["PromoId"].ToString();
            if (Request.Files.Count > 0)
            {
                var amazon = new AmazonService();
                for (var i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    var filename = Path.GetFileName(file.FileName);

                    var tempFilePath = Server.MapPath("~/TempUpload/" + filename);
                    var s3Path = string.Format("images/{0}", Guid.NewGuid()); 

                    file.SaveAs(tempFilePath);
                    using (var fileIO = System.IO.File.OpenRead(tempFilePath))
                    {
                        using (MemoryStream tempFileStream = new MemoryStream())
                        {
                            fileIO.CopyTo(tempFileStream);
                            amazon.S3Upload(s3Path, MimeMapping.GetMimeMapping(file.FileName), tempFileStream);
                        }
                    }
                    System.IO.File.Delete(tempFilePath);

                    var promo = await _promoRepository.Get(promoId);
                    if (promo.Images == null)
                    {
                        promo.Images = new List<PromoImages>();
                    }
                    promo.Images.Add(new PromoImages
                    {
                        Filename = filename,
                        IsMain = false,
                        Path = s3Path,
                        Sort = 0
                    });
                    await _promoRepository.Update(promoId, promo);
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

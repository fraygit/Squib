using MongoDB.Bson;
using Squib.Backoffice.Models;
using Squib.Data.Interface;
using Squib.Data.Model;
using Squib.Data.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Squib.Backoffice.Controllers
{
    public class RegisterController : Controller
    {
        private IUserRepository _userRepository;
        private IOrganisationRepository _organisationRepository;

        public RegisterController(IUserRepository _userRepository, IOrganisationRepository _organisationRepository)
        {
            this._userRepository = _userRepository;
            this._organisationRepository = _organisationRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> RegisterUser(ReqRegisterUser request)
        {
            var findExistingEmail = await _userRepository.GetUser(request.Email);
            if (findExistingEmail == null)
            {
                var organisations = new List<ObjectId>();
                if (!string.IsNullOrEmpty(request.OrganisationName))
                {
                    var newOrganisation = new Organisation
                    {
                        Name = request.OrganisationName
                    };
                    await _organisationRepository.CreateSync(newOrganisation);
                    organisations.Add(newOrganisation.Id);
                }
                
                var user = new User
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    VerificationCode = ObjectId.GenerateNewId().ToString(),
                    Password = SquibCrypto.HashSha256(request.Password),
                    Organisations = organisations
                };
                await _userRepository.CreateSync(user);


                return Json(new JsonGenericResult
                {
                    IsSuccess = true,
                    Message = user.Id.ToString()
                });
            }
            return Json(new JsonGenericResult
            {
                IsSuccess = false,
                Message = "Email with the same user already exists."
            });

        }

    }
}

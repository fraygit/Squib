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

        public RegisterController(IUserRepository _userRepository)
        {
            this._userRepository = _userRepository;
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
                var user = new User
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    VerificationCode = ObjectId.GenerateNewId().ToString(),
                    Password = SquibCrypto.HashSha256(request.Password)
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

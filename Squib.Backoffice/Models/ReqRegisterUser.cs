using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Squib.Backoffice.Models
{
    public class ReqRegisterUser
    {
        public string Email { get; set; }
        public string OrganisationName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
    }
}
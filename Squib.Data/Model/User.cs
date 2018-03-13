using MongoDB.Bson;
using Squib.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squib.Data.Model
{
    public class User : MongoEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePhoto { get; set; }
        public string ProfilePhotoFilename { get; set; }
        public string Status { get; set; } 
        public string VerificationCode { get; set; }
        public string CurrentOrganisation { get; set; }
        public List<ObjectId> Organisations { get; set; }
    }
}

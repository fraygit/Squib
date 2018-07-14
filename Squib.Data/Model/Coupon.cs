using MongoDB.Bson;
using Squib.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squib.Data.Model
{
    public class Coupon : MongoEntity
    {
        public string User { get; set; }
        public ObjectId PromoId { get; set; }
        public DateTime DateExpires { get; set; }
        public ObjectId TransactionId { get; set; }
        public string Status { get; set; }
    }
}

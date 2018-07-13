using MongoDB.Bson;
using Squib.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squib.Data.Model
{
    public class PaymentTransaction : MongoEntity
    {
        public string PaymentId { get; set; }
        public ObjectId PromoId { get; set; }
        public string User { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Status { get; set; }
    }
}

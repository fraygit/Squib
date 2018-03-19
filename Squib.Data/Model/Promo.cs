using MongoDB.Bson;
using Squib.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squib.Data.Model
{
    public class Promo : MongoEntity
    {
        public ObjectId OrganisationId { get; set; }
        public bool IsPublished { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int MaxNumberOfVoucher { get; set; }
        public string Description { get; set; }
        public List<PromoImages> Images { get; set; }
        public string Address { get; set; }
    }
}

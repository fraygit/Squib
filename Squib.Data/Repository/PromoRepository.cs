using MongoDB.Bson;
using MongoDB.Driver;
using Squib.Data.Interface;
using Squib.Data.Model;
using Squib.Data.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squib.Data.Repository
{
    public class PromoRepository : EntityService<Promo>, IPromoRepository
    {
        public async Task<List<Promo>> GetByOrg(ObjectId organisationId)
        {
            var builder = Builders<Promo>.Filter;
            var filter = builder.Eq("OrganisationId", organisationId);
            var listings = await ConnectionHandler.MongoCollection.Find(filter).ToListAsync();
            return listings;
        }
    }
}

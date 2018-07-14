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
    public class CouponRepository : EntityService<Coupon>, ICouponRepository
    {
        public async Task<List<Coupon>> GetByUser(string user)
        {
            var builder = Builders<Coupon>.Filter;
            var filter = builder.Eq("User", user);
            var listings = await ConnectionHandler.MongoCollection.Find(filter).ToListAsync();
            return listings;
        }
    }
}

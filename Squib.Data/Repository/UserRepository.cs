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
    public class UserRepository : EntityService<User>, IUserRepository
    {
        public async Task<User> GetUser(string username)
        {
            try
            {
                var builder = Builders<User>.Filter;
                var filter = builder.Eq("Email", username.ToLower());
                var users = await ConnectionHandler.MongoCollection.Find(filter).ToListAsync();
                if (users.Any())
                    return users.FirstOrDefault();
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

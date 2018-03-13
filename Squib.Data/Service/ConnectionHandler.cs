using MongoDB.Driver;
using Squib.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squib.Data.Service
{
    public class ConnectionHandler<T> where T : IMongoEntity
    {
        public IMongoCollection<T> MongoCollection { get; set; }

        public ConnectionHandler(string connectionString)
        {
            try
            {
                var mongoClient = new MongoClient(connectionString);

                var defaultDb = ConfigurationManager.AppSettings["DB"];
                var db = mongoClient.GetDatabase(defaultDb);

                MongoCollection = db.GetCollection<T>(typeof(T).Name.ToLower() + "s");
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to connect to the database. Please contact your administrator.");
            }

        }
    }
}

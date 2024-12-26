using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpEgitimKampi601.Services
{
    public class MongoDbConnection
    {
        private IMongoDatabase _database;    // _database diye field örnekledim

        public MongoDbConnection()   //yapıcı metod oluşturduk
        {
            var client = new MongoClient("mongodb://localhost:27017/");  // client benim isteğimi gerçekleştirecek urli tutyor
            _database = client.GetDatabase("Db601Customer");   // oluşturduğumuz _database adlı field örneğine atama yaptım.

        }
        public IMongoCollection<BsonDocument> GetCustomersCollection()
        {
            return _database.GetCollection<BsonDocument>("Customers");
        }

    }
}

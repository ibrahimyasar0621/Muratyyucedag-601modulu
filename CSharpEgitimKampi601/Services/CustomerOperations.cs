using CSharpEgitimKampi601.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpEgitimKampi601.Services
{
    public class CustomerOperations
    {
        public void AddCustomer(Customer customer)
        {
            var connection = new MongoDbConnection();    // MONGODB YE BAĞLANMA İSTEĞİNDE BULUNDUM.
            var customerCollection = connection.GetCustomersCollection(); // ARDINDAN TABLOYA BAĞLANDIK.

            var document = new BsonDocument
            {
                {"CustomerName" , customer.CustomerName },
                {"CustomerSurname" , customer.CustomerSurname  },
                {"CustomerCity", customer.CustomerCity},
                {"CustomerBalance", customer.CustomerBalance },
                {"CustomerShoppingCount", customer.CustomerShoppingCount }
            };                                                          // PARAMETRELERİ GÖNDERDİK.
            customerCollection.InsertOne(document);  // EN SONDA EKLEME İŞLEMİNİ YAPTIK.
        }

        public List<Customer> GetAllCustomer()
        {
            var connection =new MongoDbConnection(); // connection değişkeni oluşturup . veri tabanına bağlantımızı oluşturduk.
            var customerCollection = connection.GetCustomersCollection();  // customerCollection oluşturduk bu ise MONGODB deki koleksiyonumuza erişiyor. diğer ismiyle (MSSQL eeki tablo)
            var cutomers = customerCollection.Find(new BsonDocument()).ToList(); //customer koleksiyonundaki verileri hafızaya aldık. 
            List<Customer> customerList = new List<Customer>();  // Bellekte boş bir customer list oluşturduk
            foreach (var c in cutomers)                          // ardından burada bir döngü oluşturduk . bu döngünün veri çekeceği yer 34. satırdaki customers değişkeni.
            {
                customerList.Add(new Customer                    // yukarıda oluştuurduğumuz boş listeye aşağıda yazdığımız verileri bu dögüyle atadık. 
                {
                    CustomerId = c["_id"].ToString(),
                    CustomerBalance = decimal.Parse(c["CustomerBalance"].ToString()),
                    CustomerCity = c["CustomerCity"].ToString(),
                    CustomerName = c["CustomerName"].ToString(),
                    CustomerSurname = c["CustomerSurname"].ToString(),
                    CustomerShoppingCount =int.Parse( c["CustomerShoppingCount"].ToString()),
                });
            }

            return customerList;   // geriye yukarıda oluşturduğumuz boş liste customerList i dödürüp gösteriyor
        }

        public void DeleteCustomer(string id) 
        {
            var connection = new MongoDbConnection();
            var customerCollection = connection.GetCustomersCollection();
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            customerCollection.DeleteOne(filter);
        }

        public void UpdateCustomer(Customer customer) 
        {
            var connection = new MongoDbConnection();
            var customerCollection=connection.GetCustomersCollection();
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(customer.CustomerId));
            var updatedValue = Builders<BsonDocument>.Update
                .Set("CustomerName", customer.CustomerName)
                .Set("CustomerSurname", customer.CustomerSurname)
                .Set("CustomerCity", customer.CustomerCity)
                .Set("CustomerBalnce", customer.CustomerBalance)
                .Set("CustomerShppingCount", customer.CustomerShoppingCount);
            customerCollection.UpdateOne(filter, updatedValue);
        }

        public Customer GetCustomerById(string id) 
        {
            var connection = new MongoDbConnection();
            var customerCollection= connection.GetCustomersCollection();
            var filter =Builders<BsonDocument>.Filter.Eq("_id",ObjectId.Parse(id));
            var result = customerCollection.Find(filter).FirstOrDefault();
            return new Customer
            {
                CustomerBalance = decimal.Parse(result["CustomerBalance"].ToString()),
                CustomerCity = result["CustomerCity"].ToString(),
                CustomerId = id,
                CustomerName = result["CustomerName"].ToString(),
                CustomerShoppingCount = int.Parse(result["CustomerShoppingCount"].ToString()),
                CustomerSurname = result["CustomerSurname"].ToString()

            };
        }


    }
}

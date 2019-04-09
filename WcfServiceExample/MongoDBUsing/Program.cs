using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System.Configuration;
using MongoDB.Driver;
//using DemoCSharpAndMongoDB;

namespace MongoDBUsing
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoDAO<Person> mongoDAO = new MongoDAO<Person>("Person");

            //Random number = new Random();
            //string[] nameArr = new string[] { "Alex", "Viktor", "Oleg", "Olga", "Elena", "Viktoria", "Vazgen" };
            //for (int i = 0; i < 10; i++)
            //{
            //    Person person = new Person()
            //    {
            //        FirstName = nameArr[number.Next(0, nameArr.Count())],
            //        Old = number.Next(20, 80),
            //        AppearInDB = DateTime.Now,
            //        BankAccount = number.Next(10000, 20001),
            //    };

            //    mongoDAO.Create(person);
            //}

            Console.WriteLine("Запись прошла");
            Console.ReadKey();
        }
    }

    class MongoDAO<T> where T : Person // класс подключения к базе данных
    {
        string ConnectionString { get; set; }
        MongoClient Client { get; set; }
        IMongoDatabase Database { get; set; }
        IMongoCollection<T> Collection { get; set; }

        public MongoDAO(string collectionName)
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString; // для ConfigurationManager вручную нужно добавить ссылку System.Configuration
            Client = new MongoClient(ConnectionString);
            Database = Client.GetDatabase("test");

            try
            {
                Database.CreateCollection(collectionName);
            }
            catch { }

            Collection = Database.GetCollection<T>(collectionName);
        }

        public void FindAll()
        {
            var allDocuments = Collection.AsQueryable().ToList();
        }

        public void FindOne(string id)
        {
            var documentId = new ObjectId(id);
            var document = Collection.AsQueryable<T>().SingleOrDefault(x => x.Id == documentId);
        }

        public T FindOneV2(string id)
        {
            var documentId = new ObjectId(id);
            var builder = Builders<T>.Filter;
            var filter = builder.Eq(x => x.Id, documentId);
            return Collection.Find(filter).FirstOrDefault();
        }

        public void Create(T document)
        {
            Collection.InsertOne(document);
        }
    }

    class Person
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("First name")]
        public string FirstName { get; set; }

        [BsonElement("Old")]
        public int Old { get; set; }

        [BsonElement("The time of appear in datebase")]
        public DateTime AppearInDB { get; set; }

        [BsonElement("Bank account")]
        public decimal BankAccount { get; set; }
    }
}

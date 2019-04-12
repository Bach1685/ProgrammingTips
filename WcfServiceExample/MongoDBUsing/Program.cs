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

            //mongoDAO. // вызваем метод для работы с БД

            Console.WriteLine("Нажмите любую кнопку, чтобы завершить");
            Console.ReadKey();
        }
    }

    class MongoDAO<T> where T : Person // класс подключения к базе данных
    {
        string ConnectionString { get; set; } // строка подключения, в которой адрес сервера, пароль и т.д.
        MongoClient Client { get; set; } 
        IMongoDatabase Database { get; set; }
        IMongoCollection<T> Collection { get; set; }

        public MongoDAO(string collectionName)
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString; // для ConfigurationManager вручную нужно добавить ссылку System.Configuration
            //ConnectionString берется из App.config 
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

        public void FindFragment(string keyWord)
        {
            var document = Collection.AsQueryable<T>().Where(x => x.FirstName.Contains(keyWord)).ToList();
        }

        public int Sum()
        {
            var sum = Collection.AsQueryable<T>().Sum(x => x.Old);
            return sum;
        }

        public void Create(T document)
        {
            Collection.InsertOne(document);
        }

        public void Update(string id)
        {
            Collection.UpdateOne(Builders<T>.Filter.Eq("_id", ObjectId.Parse(id)), Builders<T>.Update.Set("Old", 30));
        }

        public void Delete(string id)
        {
            Collection.DeleteOne(Builders<T>.Filter.Eq("_id", ObjectId.Parse(id)));
        }
    }

    class Person // класс сериализации в json 
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

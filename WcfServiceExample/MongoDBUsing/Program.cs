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

namespace MongoDBUsing
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoDAO<Game> mongoGame = new MongoDAO<Game>();
            MongoDAO<Men> mongoMen = new MongoDAO<Men>();

            Game game = new Game { Status = "1", GameName = "rd" };
            Men men = new Men { Name = "Igor", Old = 30 };
            if(true)
            { }


            mongoMen.Create(men);
            mongoGame.Create(game);
            
            Console.WriteLine("Запись прошла");
            Console.ReadKey();
        }
    }

    class MongoDAO<T>// where T: Game // класс подключения к базе данных
    {
        string ConnectionString { get; set; }
        MongoClient Client { get; set; }
        IMongoDatabase Database { get; set; }
        //IMongoCollection<MongoDocument> mongoCollection { get; set; }
        IMongoCollection<Men> mongoCollectionFirst { get; set; }
        IMongoCollection<T> mongoCollectionSecond { get; set; }
        T game { get; set; }

        public MongoDAO()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString; // для ConfigurationManager вручную нужно добавить ссылку System.Configuration
            Client = new MongoClient(ConnectionString);
            Database = Client.GetDatabase("test");
            mongoCollectionFirst = Database.GetCollection<Men>("users");
            mongoCollectionSecond = Database.GetCollection<T>("users");
        }

        public void Create(T doc)
        {
            //MongoDocument document = new MongoDocument { Name = "Igor", Old = 30 };
            //mongoCollection.InsertOne(document);

            mongoCollectionSecond.InsertOne(doc);
        }
    }

    class Men
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("old")]
        public int Old { get; set; }
    }

    class Game
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("gameName")]
        public string GameName { get; set; }

        [BsonElement("status")]
        public string Status { get; set; }
    }
}

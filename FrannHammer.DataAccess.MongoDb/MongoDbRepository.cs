using System.Collections.Generic;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Utility;
using MongoDB.Driver;
using System.Linq;
using FrannHammer.Domain.Contracts;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace FrannHammer.DataAccess.MongoDb
{
    public interface IModelProvider
    {
        T Create<T>() where T : IModel;
    }
    public class MongoDbRepository<T> : IRepository<T>
          where T : class, IModel
    {
        private readonly IMongoDatabase _mongoDatabase;
        private const string KeyId = "_id";
        private const string KeyName = "name";

        public MongoDbRepository(IMongoDatabase mongoDatabase)
        {
            Guard.VerifyObjectNotNull(mongoDatabase, nameof(mongoDatabase));
            _mongoDatabase = mongoDatabase;
        }

        public T GetById(string id)
        {
            Guard.VerifyStringIsNotNullOrEmpty(id, nameof(id));
            var objectId = new ObjectId(id);

            var filter = Builders<BsonDocument>.Filter.Eq(KeyId, objectId);
            var raw = _mongoDatabase.GetCollection<BsonDocument>(typeof(T).Name).Find(filter).SingleOrDefault();

            return DeserializeWithId(raw);
        }

        public T GetByName(string name)
        {
            Guard.VerifyStringIsNotNullOrEmpty(name, nameof(name));

            var filter = Builders<BsonDocument>.Filter.Eq(KeyName, name);
            var raw = _mongoDatabase.GetCollection<BsonDocument>(typeof(T).Name).Find(filter).SingleOrDefault();

            return DeserializeWithId(raw);
        }

        public IEnumerable<T> GetAll()
        {
            var rawCollection = _mongoDatabase.GetCollection<BsonDocument>(typeof(T).Name).AsQueryable().ToList();

            var col = rawCollection.Select(DeserializeWithId);

            return col;
        }

        public void Update(T model)
        {
            Guard.VerifyObjectNotNull(model, nameof(model));
            Guard.VerifyStringIsNotNullOrEmpty(model.Id, nameof(model.Id));

            var objectId = new ObjectId(model.Id);

            var replaceResult = _mongoDatabase.GetCollection<T>(typeof(T).Name).ReplaceOne(Builders<T>.Filter.Eq(KeyId, objectId), model);
        }

        public void Delete(string id)
        {
            Guard.VerifyStringIsNotNullOrEmpty(id, nameof(id));
            var objectId = new ObjectId(id);
            var filter = Builders<T>.Filter.Eq(KeyId, objectId);
            _mongoDatabase.GetCollection<T>(typeof(T).Name).DeleteOne(filter);
        }

        public T Add(T model)
        {
            var objectId = ObjectId.GenerateNewId();
            model.Id = objectId.ToString();

            var collection = _mongoDatabase.GetCollection<T>(typeof(T).Name);
            collection.InsertOne(model);

            return collection.Find(Builders<T>.Filter.Eq(KeyId, objectId)).FirstOrDefault();
        }

        public void AddMany(IEnumerable<T> models)
        {
            Guard.VerifyObjectNotNull(models, nameof(models));

            var modelsList = models.ToList();
            foreach (var model in modelsList)
            {
                model.Id = ObjectId.GenerateNewId().ToString();
            }

            _mongoDatabase.GetCollection<T>(typeof(T).Name).InsertMany(modelsList);
        }

        private static T DeserializeWithId(BsonDocument rawDocument)
        {
            if (rawDocument == null)
            { return default(T); }

            var model = BsonSerializer.Deserialize<T>(rawDocument);
            model.Id = rawDocument[KeyId].ToString();
            return model;
        }
    }
}

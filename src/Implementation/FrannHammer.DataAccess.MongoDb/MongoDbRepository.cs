using System;
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
            var result = _mongoDatabase.GetCollection<BsonDocument>(typeof(T).Name).Find(filter).SingleOrDefault();
            if (result == null)
            {
                throw new ResourceNotFoundException($"Resource of type '{typeof(T).Name}' with id '{id}' not found.");
            }
            return DeserializeWithId(result);
        }

        public T GetByName(string name)
        {
            Guard.VerifyStringIsNotNullOrEmpty(name, nameof(name));

            var filter = Builders<BsonDocument>.Filter.Eq(KeyName, name);
            var result = _mongoDatabase.GetCollection<BsonDocument>(typeof(T).Name).Find(filter).SingleOrDefault();

            if (result == null)
            {
                throw new ResourceNotFoundException($"Resource of type '{typeof(T).Name}' with name '{name}' not found.");
            }
            return DeserializeWithId(result);
        }

        public T GetSingleWhere(Func<T, bool> where)
        {
            var result = _mongoDatabase.GetCollection<T>(typeof(T).Name).AsQueryable().SingleOrDefault(where);
            if (result == null)
            {
                throw new ResourceNotFoundException($"Resource of type '{typeof(T).Name}' not found.");
            }
            return result;
        }

        public IEnumerable<T> GetAllWhere(Func<T, bool> where)
        {
            var rawCollection = _mongoDatabase.GetCollection<T>(typeof(T).Name).AsQueryable().Where(where).ToList();
            return rawCollection;
        }

        public IEnumerable<T> GetAllWhere(IDictionary<string, object> queryParameters)
        {
            Guard.VerifyObjectNotNull(queryParameters, nameof(queryParameters));

            if (queryParameters.Count == 0)
            {
                throw new InvalidOperationException($"Must specify at least one query parameter in {queryParameters}");
            }

            var filterDefinition = default(FilterDefinition<BsonDocument>);

            foreach (var kvp in queryParameters)
            {
                if (filterDefinition == null)
                {
                    filterDefinition = Builders<BsonDocument>.Filter.Eq(kvp.Key, kvp.Value);
                    continue;
                }
                if (kvp.Value is string)
                {
                    filterDefinition = filterDefinition &
                                       Builders<BsonDocument>.Filter.Regex(kvp.Key,
                                           new BsonRegularExpression($"^{kvp.Value}$", "i"));
                }
                else
                {
                    filterDefinition = filterDefinition & Builders<BsonDocument>.Filter.Eq(kvp.Key, kvp.Value);
                }
            }

            var rawCollection =
                _mongoDatabase.GetCollection<BsonDocument>(typeof(T).Name).Find(filterDefinition).ToList();

            return DeserializeWithId(rawCollection);
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
            Guard.VerifyStringIsNotNullOrEmpty(model.InstanceId, nameof(model.InstanceId));

            var objectId = new ObjectId(model.InstanceId);

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
            model.InstanceId = objectId.ToString();

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
                model.InstanceId = ObjectId.GenerateNewId().ToString();
            }

            _mongoDatabase.GetCollection<T>(typeof(T).Name).InsertMany(modelsList);
        }

        private static T DeserializeWithId(BsonDocument rawDocument)
        {
            if (rawDocument == null)
            { return default(T); }

            var model = BsonSerializer.Deserialize<T>(rawDocument);
            //model.InstanceId = rawDocument[KeyId].ToString();
            return model;
        }

        private static IEnumerable<T> DeserializeWithId(IEnumerable<BsonDocument> rawDocuments)
        {
            return rawDocuments.Select(DeserializeWithId);
        }
    }
}

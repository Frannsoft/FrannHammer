using System;
using System.Collections.Generic;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using MongoDB.Driver;

namespace FrannHammer.DataAccess.MongoDb
{
    public class MongoDbRepository<T> : IRepository<T>
          where T : IModel
    {
        private readonly IMongoDatabase _mongoDatabase;
        private const string KeyId = "_id";

        public MongoDbRepository(IMongoDatabase mongoDatabase)
        {
            Guard.VerifyObjectNotNull(mongoDatabase, nameof(mongoDatabase));
            _mongoDatabase = mongoDatabase;
        }

        public T Get(int id)
        {
            var filter = Builders<T>.Filter.Eq(KeyId, id);
            var raw = _mongoDatabase.GetCollection<T>(typeof(T).Name).Find(filter).SingleOrDefault();
            return raw;
        }

        public IEnumerable<T> GetAll()
        {
            return _mongoDatabase.GetCollection<T>(typeof(T).Name).AsQueryable().ToList();
        }

        public T Update(T model)
        {
            throw new NotImplementedException();
        }

        public void Delete(T model)
        {
            var filter = Builders<T>.Filter.Eq(KeyId, model.Id);
            _mongoDatabase.GetCollection<T>(typeof(T).Name).DeleteOne(filter);
        }

        public T Add(T model)
        {
            _mongoDatabase.GetCollection<T>(typeof(T).Name).InsertOne(model);
            return model;
        }

        public void AddMany(IEnumerable<T> models)
        {
            Guard.VerifyObjectNotNull(models, nameof(models));
            _mongoDatabase.GetCollection<T>(typeof(T).Name).InsertMany(models);
        }
    }
}
